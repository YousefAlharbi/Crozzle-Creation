using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Main
    {
        private void WordListDataValidation()
        {
            // word numbers provided in header
            WORD_NUM = 0;
            // crozzle width provided in header
            crozzle_W = 0;
            // crozzle height provided in header
            crozzle_H = 0;
            // game difficulty provided in header. value 1 for easy and 2 for hard.
            GAME_D = 0;

            //header error message counter 
            ERROR_COUNT_H = 0;
            // wordlist error message counter
            ERROR_COUNT_W = 0;
            //header error message logger string
            ERROR_LOG_H = "";
            // worldlist error message logger
            ERROR_LOG_W = "";
            //default no error occured
            ERROR = false;

            string data = string.Empty;
            int rows, cols = 0;

            // read all rows serially
            for (rows = 0; rows < (wordDataGridView.Rows.Count); rows++)
            {
                // read all columns of specific row, serially
                for (cols = 0; cols < (wordDataGridView.Columns.Count); cols++)
                {
                    try
                    {
                        // get the from specific grid cell, we want to work
                        data = wordDataGridView.Rows[rows].Cells[cols].Value.ToString();

                        //check, if it is the first/header row
                        if (rows == 0)
                        {
                            // check if it is the header colums 1 or 2 or 3
                            if (cols == 0 || cols == 1 || cols == 2)
                            {
                                try
                                {
                                    //if data string length is greater then 0 
                                    if (data.Length > 0)
                                    {
                                        // if this is the total word number providing column
                                        if (cols == 0)
                                        {
                                            WORD_NUM = int.Parse(data);

                                            // if given number is greater than 500, it will log error.
                                            if (WORD_NUM > 500)
                                                WordListErrorCollection("Header of Word list can't contain value more than 500." + Environment.NewLine, rows, cols, 1);

                                        }
                                        // check if this is the crozzle width providing column
                                        else if (cols == 1)
                                        {
                                            crozzle_W = int.Parse(data);

                                            // if given number is greater than 100 or less then 5, it will log error.
                                            if (crozzle_W < 5 || crozzle_W > 100)
                                                WordListErrorCollection("Given Crozzle width is " + crozzle_W
                                                    + ". But crozzle width can't be less than 5 or greater than 100." + Environment.NewLine, rows, cols, 1);
                                        }
                                        // check if this is the crozzle height providing column
                                        else if (cols == 2)
                                        {
                                            crozzle_H = int.Parse(data);

                                            // if given number is greater than 100 or less then 5, it will log error.
                                            if (crozzle_H < 5 || crozzle_H > 100)
                                                WordListErrorCollection("Given Crozzle height is " + crozzle_H
                                                    + ". But crozzle height can't be less than 5 or greater than 100." + Environment.NewLine, rows, cols, 1);
                                        }
                                    }
                                    // if string length is 0, it will log an error of empty data in data expected header column. 
                                    else
                                    {
                                        WordListErrorCollection(w_ERRORx[cols * 2], rows, cols, 1);
                                    }
                                }
                                catch (Exception)
                                {
                                    WordListErrorCollection(w_ERRORx[cols * 2 + 1], rows, cols, 1);
                                }
                            }
                            // check if this is the game difficulty providing column
                            else if (cols == 3)
                            {
                                try
                                {
                                    // check if data is not empty
                                    if (data.Length == 0)
                                    {
                                        WordListErrorCollection(w_ERRORx[6], rows, cols, 1);
                                    }
                                    else if (string.Equals(data, "EASY", StringComparison.OrdinalIgnoreCase)) GAME_D = 1;
                                    else if (string.Equals(data, "HARD", StringComparison.OrdinalIgnoreCase)) GAME_D = 2;
                                    else
                                    {
                                        WordListErrorCollection(@"""" + data + @"""" + w_ERRORx[7], rows, cols, 1);
                                    }
                                }
                                catch (Exception)
                                {
                                    WordListErrorCollection(w_ERRORx[6], rows, cols, 1);
                                }
                            }
                            else
                            {
                                //check if unncessary data is provided or not, in header row
                                if (data.Length != 0 || !string.Equals(data, ""))
                                {
                                    WordListErrorCollection(data + " is in Row: " + (rows) + " & Column: " + (cols)
                                        + " -> But this" + w_ERRORx[11], rows, cols, 2);
                                }
                            }
                        }
                        // check if this is the first column but not of header. Actulayy this column is containing words.
                        else if (cols == 0 && rows != 0)
                        {
                            // check if the data of word cell is not empty.
                            if (data.Length == 0 || string.Equals(data, ""))
                            {
                                // if word cell is empty it will log an error.
                                WordListErrorCollection("Row: " + (rows) + " & Column: " + (cols) + " -> this" + w_ERRORx[8], rows, cols, 2);
                            }
                            else
                            {
                                // a valid word found, and add it to our word list database.
                                Dictionary word = new Dictionary();
                                word.word = data;
                                word.used = false;
                                WORD_LIST.Add(word);
                            }
                        }
                        else
                        {
                            //check if unncessary data is provided or not, in other rows of word lists
                            if (data.Length != 0 || !string.Equals(data, ""))
                            {
                                // if unneccessary words is found, it will log an error.
                                WordListErrorCollection( @"""" + data + @"""" + " is found in Row: " + (rows) + " & Column: " + (cols)
                                    + " -> But this" + w_ERRORx[11], rows, cols, 2);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                // calculate total data progress
                cols = 0;
                float value = (float)rows / (float)wordDataGridView.Rows.Count;
                value *= 50;
                Console.WriteLine((int)value);
                //wordDataGridView.Invoke((MethodInvoker)delegate
                //{
                //    wordDataGridView.Rows[rows].HeaderCell.Value = (rows).ToString();
                //});
                progressBar.BeginInvoke(new MethodInvoker(() => { progressBar.Value = (int)value + 50; }));

                // wait for 60 milisecond for UI/other thread to take place in execution to avoid UI freeze.
                Thread.Sleep(60);
            }

            // check is provided word number in header is greater or not than the number of actual words given in file
            if (WORD_NUM > WORD_LIST.Count)
            {
                WordListErrorCollection(w_ERRORx[9], -1, -1, 2);
            }
            // check is provided word number in header is less or not than the number of actual words given in file
            else if (WORD_NUM < WORD_LIST.Count)
            {
                WordListErrorCollection(w_ERRORx[10], -1, -1, 2);
            }

            //check if word list not exceeds 500 word count. or show error.
            if (WORD_LIST.Count > 500)
            {
                WordListErrorCollection("Word list can't contain more than 500 words." + Environment.NewLine, -1, -1, 2);
            }

            // this will show the error log message calculated from all word list data.
            this.Invoke((MethodInvoker)delegate
            {
                messageLabel.Text = "Message: File validation process has finished. Open a crozzle now.";
                ShowWordValidationMessage();
            });
        }

        private void ShowWordValidationMessage()
        {
            // if any error found, it will log this and show in error dialog.
            if (ERROR)
            {
                // put everything in initialize state state.
                WORD_LIST.Clear();
                WORD_NUM = 0;
                crozzle_W = 0;
                crozzle_H = 0;
                GAME_D = 0;
                MessageBox.Show("Violations:" + Environment.NewLine + Environment.NewLine + "Header Violations:" + Environment.NewLine + ERROR_LOG_H
                            + Environment.NewLine + "Word aspect Violations:" + Environment.NewLine + ERROR_LOG_W, "CSV file contains invalid data!!!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                SaveWordLog("Violations:" + Environment.NewLine + Environment.NewLine + "Header Violations:" + Environment.NewLine
                    + ERROR_LOG_H + Environment.NewLine + "Word aspect Violations:" + Environment.NewLine + ERROR_LOG_W);
            }
            // if no error, then programm will proceed to next step of choosing new croozle
            else
            {
                //wordDataGridView.Visible = true;
                MessageBox.Show("Validation complete." +
                                Environment.NewLine + "1. Word count: " + WORD_NUM +
                                Environment.NewLine + "2. Crozzle width: " + crozzle_W +
                                Environment.NewLine + "3. Crozzle height: " + crozzle_H +
                                Environment.NewLine + "4. Difficulty level: " + DifficultyLevel(GAME_D),
                                "Data load successfull.",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //save errors in log file
        public void SaveWordLog(string log)
        {
            if (Directory.Exists(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Log Files") == false)
            {
                Directory.CreateDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Log Files");
            }
            //System.IO.File.WriteAllText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName
            //     + "\\Log Files\\word_list_error_log.txt", log);
            string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName
                 + "\\Log Files\\word_list_error_log.txt";

            if (!File.Exists(filePath))
            {
                FileStream aFile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine("Rcorded at: " + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz") + Environment.NewLine + Environment.NewLine
                    + "File Name: " + FILE_NAME + Environment.NewLine + Environment.NewLine
                    + log + Environment.NewLine + Environment.NewLine + "/***************************************************************/");
                sw.Close();
                sw.Close();
                aFile.Close();
            }
            else
            {
                FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine("Rcorded at: " + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz") + Environment.NewLine + Environment.NewLine
                    + "File Name: " + FILE_NAME + Environment.NewLine + Environment.NewLine
                    + log + Environment.NewLine + Environment.NewLine + "/***************************************************************/");
                sw.Close();
                sw.Close();
                file.Close();
            }
        }

        // integer value Vs. game difficulty mapping.
        private string DifficultyLevel(int dif)
        {
            if (dif == 1) return "EASY";
            else if (dif == 2) return "HARD";
            else return "Not Set";
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
        }

        // sorting columns in grid view is disabled.
        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        // if an error occurs, this will change the grid cell color to red.
        private void WordListErrorCollection(string error_message, int row, int col, int error_type)
        {
            ERROR = true;
            if (error_type == 1)
            {
                ERROR_COUNT_H++;
                // add new error message to error logging string
                ERROR_LOG_H = ERROR_LOG_H + "#Violation " + ERROR_COUNT_H.ToString() + " -> " + error_message;
            }
            else if (error_type == 2)
            {
                ERROR_COUNT_W++;
                // add new error message to error logging string
                ERROR_LOG_W = ERROR_LOG_W + "#Violation " + ERROR_COUNT_W.ToString() + " -> " + error_message;
            }
            //if (row >= 0 && col >= 0) wordDataGridView.Rows[row].Cells[col].Style = ERROR_STYLE;
        }

        public class Dictionary
        {
            public string word;
            public bool used;
        }
    }
}
