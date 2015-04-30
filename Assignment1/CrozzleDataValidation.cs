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
        List<Word> word_found;

        private int CrozzleListDataValidation()
        {
            // wordlist error message counter
            ERROR_COUNT_CROZZLE = 0;
            // worldlist error message logger
            ERROR_LOG_CROZZLE = "";
            //default no error occured
            ERRORZ_CROZZLE = false;

            //set the point value to zero
            GAME_POINTS = 0;

            // declare a empty string
            string data = string.Empty;

            //initialize crozzled alphabates 2D string
            CROZZLE2D_ALPHABATES = new string[crozzleDataGridView.Rows.Count - 1][];

            // initialize word found firstly
            word_found = new List<Word>();

            //through error for exceeding given height in word list.
            if (crozzleDataGridView.Rows.Count - 1 != crozzle_W)
                CrozzleErrorCollection("Given height in word list does not match with given crozzle height." + Environment.NewLine);

            //through error for exceeding given width in word list.
            if (crozzleDataGridView.Columns.Count != crozzle_H)
                CrozzleErrorCollection("Given width in word list does not match with given crozzle width." + Environment.NewLine);

            // read all rows serially
            for (int row = 0; row < crozzleDataGridView.Rows.Count-1; row++)
            {
                //declare row in string.
                CROZZLE2D_ALPHABATES[row] = new string[crozzleDataGridView.Columns.Count];

                // read all columns of specific row, serially
                for (int col = 0; col < crozzleDataGridView.Columns.Count; col++)
                {
                    // get the from specific grid cell, we want to work
                    data = crozzleDataGridView.Rows[row].Cells[col].Value.ToString();

                    //initialize a character

                    //check if any word data has provided.
                    if (data.Length == 1 && !string.Equals(data, "") && (data.ElementAt(0) >=65 && data.ElementAt(0) <=90))
                    {
                        crozzleDataGridView.Invoke((MethodInvoker)delegate
                        {
                            crozzleDataGridView.Rows[row].Cells[col].Style = ALPHABATE_STYLE;
                        });
                        CROZZLE2D_ALPHABATES[row][col] = data;
                    }
                    //check if unncessary data is provided or not, in header row
                    else if (data.Length == 0 || string.Equals(data, ""))
                    {
                        // put space if there is blank in cell.
                        CROZZLE2D_ALPHABATES[row][col] = " ";
                    }
                    else if (data.Length == 1 || !string.Equals(data, "") && !(data.ElementAt(0) >= 65 && data.ElementAt(0) <= 90))
                    {
                        // through error for invalid data.
                        CrozzleErrorCollection("Row: " + row + " & Column: " + col +
                            " -> contains invalid data as crozzle cell contains a capital english letter but not any sign or number(s)." + Environment.NewLine);
                        crozzleDataGridView.Invoke((MethodInvoker)delegate
                        {
                            crozzleDataGridView.Rows[row].Cells[col].Style = ERROR_STYLE;
                        });
                        CROZZLE2D_ALPHABATES[row][col] = data;
                    }
                    else
                    {
                        // through error for invalid data.
                        CrozzleErrorCollection("Row: " + row + " & Column: " + col +
                            " -> contains invalid data as crozzle cell contains only one capital english letter each." + Environment.NewLine);
                        crozzleDataGridView.Invoke((MethodInvoker)delegate
                        {
                            crozzleDataGridView.Rows[row].Cells[col].Style = ERROR_STYLE;
                        });
                        CROZZLE2D_ALPHABATES[row][col] = data;
                    }
                }

                // calculate total data progress
                float value = (float)row / (float)crozzleDataGridView.Rows.Count - 1;
                value *= 25;
                //Console.WriteLine((int)value);
                progressBar.BeginInvoke(new MethodInvoker(() => { progressBar.Value = (int)value + 50; }));

                // wait for 60 milisecond for UI/other thread to take place in execution to avoid UI freeze.
                Thread.Sleep(30);
            }

            string row_word = "";

            string[] col_word = new string[crozzleDataGridView.Columns.Count];
            for (int col = 0; col < crozzleDataGridView.Columns.Count; col++) col_word[col] = "";

            //parse the words and validate them.
            for (int row = 0; row < crozzleDataGridView.Rows.Count - 1; row++)
            {
                for (int col = 0; col < crozzleDataGridView.Columns.Count; col++)
                {
                    // if this is a letter then it will pushed to row and column stack
                    if(CROZZLE2D_ALPHABATES[row][col].Length == 1 && !string.Equals(CROZZLE2D_ALPHABATES[row][col], " "))
                    {
                        row_word = row_word + CROZZLE2D_ALPHABATES[row][col];
                        col_word[col] = col_word[col] + CROZZLE2D_ALPHABATES[row][col];
                    }
                    // if this is a space character then it will check that if row and column stack contains a word or not
                    else if (CROZZLE2D_ALPHABATES[row][col].Length == 1 && string.Equals(CROZZLE2D_ALPHABATES[row][col], " "))
                    {
                        if (row_word.Length > 1)
                        {
                            //Console.WriteLine(row_word);
                            InsertWord(row_word, row, col - row_word.Length, 1);
                        }
                        if (col_word[col].Length > 1) 
                        {
                            //Console.WriteLine(col_word[col]);
                            InsertWord(col_word[col], row - col_word[col].Length, col, 2);
                        }

                        //reset the stacks
                        row_word = "";
                        col_word[col] = "";
                    }
                    //Console.Write(CROZZLE2D_ALPHABATES[row][col]);
                }

                // as the row is finished reading, check if the row stack contains any word from last line
                if (row_word.Length > 1)
                {
                    //Console.WriteLine(row_word);
                    InsertWord(row_word, row, crozzleDataGridView.Columns.Count - row_word.Length, 1);
                }

                //reset the row stack as this is the begining of new row.
                row_word = "";

                // calculate total data progress
                float value = (float)row / (float)crozzleDataGridView.Rows.Count - 1;
                value *= 15;
                //Console.WriteLine((int)value);
                progressBar.BeginInvoke(new MethodInvoker(() => { progressBar.Value = (int)value + 75; }));

                // wait for 30 milisecond for UI/other thread to take place in execution to avoid UI freeze.
                Thread.Sleep(30);
            }

            // when break from the loop, check for the last time, the presence of a word in row stack
            if (row_word.Length > 1)
            {
                //Console.WriteLine(row_word);
                InsertWord(row_word, crozzleDataGridView.Rows.Count - 1, crozzleDataGridView.Columns.Count - row_word.Length, 1);
            }

            // when break from the loop, check for the last time, the presence of a word in each column stack
            for (int col = 0; col < crozzleDataGridView.Columns.Count; col++)
            {
                if (col_word[col].Length > 1)
                {
                    //Console.WriteLine(col_word[col]);
                    InsertWord(col_word[col], crozzleDataGridView.Rows.Count - 1 - col_word[col].Length, col, 2);
                }
            }

            // cross word point calculation in HARD mode.
            // check all cross relations and calculate points
            for (int j = 0; j < word_found.Count; j++)
            {
                for (int i = 0; i < word_found.Count; i++)
                {
                    if (i != j)
                    {
                        // if this two words have any cross section, when 'word_found[j]' is horizontal and 'word_found[i]' is vertical
                        if ((word_found[i].row + word_found[i].word.Length - 1) >= word_found[j].row && word_found[i].row <= word_found[j].row
                            && (word_found[j].col + word_found[j].word.Length - 1) >= word_found[i].col
                            && word_found[j].col <= word_found[i].col && word_found[j].type != word_found[i].type && word_found[j].type == 1)
                        {
                            //Console.WriteLine(word_found[j].word + ", " + word_found[i].word + " - " + CROZZLE2D_ALPHABATES[word_found[j].row][word_found[i].col]);

                            // get the extra intersection point bonus in difficult mode
                            if (word_found[i].found && word_found[j].found && GAME_D == 2)
                            {
                                // calculate point for intersected point.
                                GAME_POINTS += GetIntersectionLetterPoint(CROZZLE2D_ALPHABATES[word_found[j].row][word_found[i].col]);
                            }
                            else if (word_found[i].found && word_found[j].found && GAME_D == 1)
                            {
                                // calculate point for intersected point.
                                GAME_POINTS -= 1;
                            }
                        }
                        // if this two words have any cross section, when 'word_found[j]' is vertical and 'word_found[i]' is horizontal
                        else if ((word_found[i].col + word_found[i].word.Length - 1) >= word_found[j].col && word_found[i].col <= word_found[j].col
                            && (word_found[j].row + word_found[j].word.Length - 1) >= word_found[i].row
                            && word_found[j].row <= word_found[i].row && word_found[j].type != word_found[i].type && word_found[j].type == 2)
                        {
                            //Console.WriteLine(word_found[j].word + ", " + word_found[i].word + " - " + CROZZLE2D_ALPHABATES[word_found[i].row][word_found[j].col]);

                            // get the extra intersection point bonus in difficult mode
                            if (word_found[i].found && word_found[j].found && GAME_D == 2)
                            {
                                // calculate point for intersected point.
                                GAME_POINTS += GetIntersectionLetterPoint(CROZZLE2D_ALPHABATES[word_found[i].row][word_found[j].col]);
                            }
                            else if (word_found[i].found && word_found[j].found && GAME_D == 1)
                            {
                                // calculate point for intersected point.
                                GAME_POINTS -= 1;
                            }
                        }
                    }
                }

                // wait for 5 milisecond for UI/other thread to take place in execution to avoid UI freeze.
                Thread.Sleep(5);
            }

            GAME_POINTS /= 2;

            // new stack for running bfs in word list.
            List<Word> word_stack = new List<Word>();

            if (word_found.Count > 0)
            {
                word_stack.Add(word_found[0]);
                word_found.RemoveAt(0);
            }
            else return 0;

            //bfs
            while (word_stack.Count > 0)
            {
                Word temp = word_stack[0];
                word_stack.RemoveAt(0);
                int is_Found = FindInDictionary(temp.word, 0);
                if (is_Found == 2)
                {
                    //through error of word repeatation in crozzle.
                    CrozzleErrorCollection("Repetition of words have found." + Environment.NewLine);
                    SetErrorStyleOnInvalidItems(temp, 2);
                    //break;
                }
                else if (is_Found == 1)
                {
                    //increase point for a new valid word
                    //Console.WriteLine("Point Increased.");

                    // for easy mode, one point for each letter
                    if (GAME_D == 1)
                    {
                        GAME_POINTS += temp.word.Length;
                    }
                    //for difficult mode 10 points per word, whatever it's length is.
                    else if (GAME_D == 2)
                    {
                        GAME_POINTS += 10;
                    }
                }
                else
                {
                    //through error of word repeatation in crozzle.
                    CrozzleErrorCollection("Invalid word has found." + Environment.NewLine);
                    SetErrorStyleOnInvalidItems(temp, 0);
                }

                for (int i = 0; i < word_found.Count; i++)
                {
                    // if this two words have any cross section, when 'temp' is horizontal and 'word_found[i]' is vertical
                    if ((word_found[i].row + word_found[i].word.Length - 1) >= temp.row && word_found[i].row <= temp.row
                        && (temp.col + temp.word.Length - 1) >= word_found[i].col 
                        && temp.col <= word_found[i].col && temp.type != word_found[i].type && temp.type == 1)
                    {
                        //Console.WriteLine(temp.word + ", " + word_found[i].word + " - " + CROZZLE2D_ALPHABATES[temp.row][word_found[i].col]);

                        //add new connected word
                        word_stack.Add(word_found[i]);
                        word_found.RemoveAt(i);
                        i--;
                    }
                    // if this two words have any cross section, when 'temp' is vertical and 'word_found[i]' is horizontal
                    else if ((word_found[i].col + word_found[i].word.Length - 1) >= temp.col && word_found[i].col <= temp.col
                        && (temp.row + temp.word.Length - 1) >= word_found[i].row
                        && temp.row <= word_found[i].row && temp.type != word_found[i].type && temp.type == 2)
                    {
                        //Console.WriteLine(temp.word + ", " + word_found[i].word + " - " + CROZZLE2D_ALPHABATES[word_found[i].row][temp.col]);
                        
                        //add new connected word
                        word_stack.Add(word_found[i]);
                        word_found.RemoveAt(i);
                        i--;
                    }
                    // check if two words are side bt side in horizontally in EASY mode.
                    else if ((temp.row == word_found[i].row - 1 || temp.row == word_found[i].row + 1)
                        && !((temp.col + temp.word.Length - 2) < word_found[i].col 
                        || temp.col > (word_found[i].col + word_found[i].word.Length-2))
                        && temp.type == word_found[i].type && temp.type == 1 && GAME_D == 1)
                    {
                        //through error for side by side word in easy mode
                        CrozzleErrorCollection("Two words are found horizontally side by side in easy mode." + Environment.NewLine);
                        //Console.WriteLine("Two words in horizontally side by side.");
                        SetErrorStyleOnInvalidItems(temp, 1);
                        //break;
                    }
                    // check if two words are side bt side in vertically in EASY mode.
                    else if ((temp.col == word_found[i].col - 1 || temp.col == word_found[i].col + 1)
                        && !((temp.row + temp.word.Length - 2) < word_found[i].row
                        || temp.row > (word_found[i].row + word_found[i].word.Length - 2))
                        && temp.type == word_found[i].type && temp.type == 2 && GAME_D == 1)
                    {
                        //through error for side by side word in easy mode
                        CrozzleErrorCollection("Two words are found vertically side by side in easy mode." + Environment.NewLine);
                        //Console.WriteLine("Two words in vertivally side by side.");
                        SetErrorStyleOnInvalidItems(temp, 1);
                        //break;
                    }
                }

                //check if disconnected grpah is found

                if (word_found.Count > 0 && word_stack.Count == 0)
                {
                    // through error for disconnected group of words.
                    CrozzleErrorCollection("This crozzle have disconnected group of words." + Environment.NewLine);

                    //start with new portion of graph
                    word_stack.Add(word_found[0]);
                    word_found.RemoveAt(0);
                }

                // wait for 30 milisecond for UI/other thread to take place in execution to avoid UI freeze.
                Thread.Sleep(30);
            }

            //set all word state in word list as unused
            for (int i = 0; i < WORD_LIST.Count; i++)
            {
                WORD_LIST[i].used = false;
            }

            // this will show the error log message calculated from all crozzle data.
            this.Invoke((MethodInvoker)delegate
            {
                //messageLabel.Text = "Message: File validation process has finished. Open a crozzle now.";
                ShowCrozzleValidationMessage();
            });
            //Console.WriteLine(GAME_POINTS);
            return GAME_POINTS;
        }

        //show red color in the cell of grid view containing invalid word
        // error type == 0 : the word was not found in dictionary. so it's invalid or pointless.
        // error type == 1 : the word has voilated the constraint of crozzle game.
        // error type == 2 : the word has found twice in the crozzle. Duplication error.
        private void SetErrorStyleOnInvalidItems(Word temp, int error_type)
        {
            for (int i = 0; i < temp.word.Length; i++)
            {
                if (temp.type == 1)
                {
                    if (error_type == 0) crozzleDataGridView.Rows[temp.row].Cells[temp.col + i].Style = INVALID_STYLE;
                    else if (error_type == 1) crozzleDataGridView.Rows[temp.row].Cells[temp.col + i].Style = CONSTRAINT_VIOLATION_STYLE;
                    else if (error_type == 2) crozzleDataGridView.Rows[temp.row].Cells[temp.col + i].Style = DUPLICATION_STYLE;
                }
                else
                {
                    if (error_type == 0) crozzleDataGridView.Rows[temp.row + i].Cells[temp.col].Style = INVALID_STYLE;
                    else if (error_type == 1) crozzleDataGridView.Rows[temp.row + i].Cells[temp.col].Style = CONSTRAINT_VIOLATION_STYLE;
                    else if (error_type == 2) crozzleDataGridView.Rows[temp.row + i].Cells[temp.col].Style = DUPLICATION_STYLE;
                }
            }
        }

        // insert a new word, parsed from crozzle file
        private void InsertWord(string word, int row, int col, int type)
        {
            Word temp_word = new Word();
            temp_word.word = word;
            temp_word.row = row;
            temp_word.col = col;
            temp_word.type = type;
            temp_word.found = SearchInDictionary(word);
            word_found.Add(temp_word);
        }

        // find the word in given word list dictionary
        private bool SearchInDictionary(string word)
        {
            for (int num = 0; num < WORD_LIST.Count; num++)
            {
                if (string.Equals(WORD_LIST[num].word, word))return true;
            }
            return false;
        }

        // find the word in given word list dictionary
        private int FindInDictionary(string word, int test)
        {
            for (int num = 0; num < WORD_LIST.Count; num++)
            {
                if (string.Equals(WORD_LIST[num].word, word))
                {
                    if (!WORD_LIST[num].used && test == 0)
                    {
                        WORD_LIST[num].used = true;
                        return 1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        // this is for calculating bonus points in HARD mode
        private int GetIntersectionLetterPoint(string intersector)
        {
            if (string.Equals(intersector, "A") || string.Equals(intersector, "E") || string.Equals(intersector, "I")
                || string.Equals(intersector, "O") || string.Equals(intersector, "U")) return 1;
            else if (string.Equals(intersector, "B") || string.Equals(intersector, "C") || string.Equals(intersector, "D")
                || string.Equals(intersector, "F") || string.Equals(intersector, "G")) return 2;
            else if (string.Equals(intersector, "K") || string.Equals(intersector, "L") || string.Equals(intersector, "M")
                || string.Equals(intersector, "H") || string.Equals(intersector, "J")) return 4;
            else if (string.Equals(intersector, "N") || string.Equals(intersector, "P") || string.Equals(intersector, "Q")
                || string.Equals(intersector, "R")) return 8;
            else if (string.Equals(intersector, "S") || string.Equals(intersector, "T") || string.Equals(intersector, "V")) return 16;
            else if (string.Equals(intersector, "W") || string.Equals(intersector, "X") || string.Equals(intersector, "Y")) return 32;
            else if (string.Equals(intersector, "Z")) return 64;
            return 0;
        }

        private void ShowCrozzleValidationMessage()
        {
            // if any error found, it will log this and show in error dialog.
            if (ERRORZ_CROZZLE)
            {
                pointLabel.Text = "Points: " + GAME_POINTS + "\n" + "Time taken: " + secondPassed + " seconds";
                if(!secCounter.Enabled)
                    MessageBox.Show("Violations:" + Environment.NewLine + ERROR_LOG_CROZZLE, "Crozzle file Violate Rules!!!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                SaveCrozzolLog("Violations:" + Environment.NewLine + ERROR_LOG_CROZZLE);
            }
            // if no error, then programm will proceed to next step of choosing new croozle again
            else
            {
                //wordDataGridView.Visible = true;
                pointLabel.Text = "Points: " + GAME_POINTS;
                if (!secCounter.Enabled)
                    MessageBox.Show("Parsing and Point Calculation complete."
                                + Environment.NewLine+ "Total Point: " + GAME_POINTS,
                                "Crozzle solving successfull.",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //save errors in log file
        public void SaveCrozzolLog(string log)
        {
            if (Directory.Exists(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Log Files") == false)
            {
                Directory.CreateDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Log Files");
            }

            string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName
                 + "\\Log Files\\crozzle_error_log.txt";

            if (!File.Exists(filePath))
            {
                FileStream aFile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine("Rcorded at: " + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz") + Environment.NewLine + Environment.NewLine
                    + "File Name: " + FILE_NAME + Environment.NewLine + Environment.NewLine
                    + log + Environment.NewLine + Environment.NewLine + "/***************************************************************/");
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

        private void crozzleDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        // sorting columns in grid view is disabled.
        private void crozzleDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        // if an error occurs, this will change the grid cell color to red.
        private void CrozzleErrorCollection(string error_message)
        {
            ERRORZ_CROZZLE = true;
            ERROR_COUNT_CROZZLE++;
            // add new error message to error logging string
            ERROR_LOG_CROZZLE = ERROR_LOG_CROZZLE + "#Violation " + ERROR_COUNT_CROZZLE.ToString() + " -> " + error_message;
        }

        private class Word
        {
            public string word;
            public int row;
            public int col;
            public int type;
            public bool found;
        }
    }
}
