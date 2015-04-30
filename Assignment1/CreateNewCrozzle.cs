using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Main
    {
        // Yousef Alharbi
        //int c_width;
        //int c_height;
        int loopC;
        private static bool shouldStop;
        PatternDatabase bestpattern;

        System.Timers.Timer secCounter;
        private static int secondPassed;

        string[] changedPattern;

        private void CreateAndShowCrozzle()
        {
            //crozzle_W = 10;
            //crozzle_H = 16;
            //GAME_D = 2;
            crozzle_W = crozzle_W ^ crozzle_H;
            crozzle_H = crozzle_W ^ crozzle_H;
            crozzle_W = crozzle_W ^ crozzle_H;

            crozzle_Array = new string[crozzle_H][];

            for (int i = 0; i < crozzle_H; i++)
            {
                crozzle_Array[i] = new string[crozzle_W];

                for (int j = 0; j < crozzle_W; j++)
                {
                    crozzle_Array[i][j] = "";
                }
            }

            /*int _cname = 0;
            foreach (string t in crozzle_Array[0])
            {
                data_Table_CreatedCrozzle.Columns.Add(_cname.ToString(), typeof(string));
                _cname++;
            }*/

            /*foreach(string t in WORD_LIST_C)
            {
                Dictionary word = new Dictionary();
                word.word = t;
                word.used = false;
                WORD_LIST.Add(word);
            }*/

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {
                CalculateBestCrozzle();
            });

            bw.RunWorkerAsync();
        }

        static IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            var sorted = from s in e
                         orderby s.Length descending
                         select s;
            return sorted;
        }

        private void CalculateBestCrozzle()
        {
            loopC = 2;
            
            bestpattern = new PatternDatabase();
            bestpattern.score = 0;
            bestpattern.board = new string[crozzle_H][];
            for (int i = 0; i < crozzle_H; i++)
            {
                bestpattern.board[i] = new string[crozzle_W];
            }

            int h_length = 0;
            changedPattern = new string[WORD_LIST_C.Length];

            if (GAME_D == 1)
            {
                int n = 0;
                foreach (string s in SortByLength(WORD_LIST_C))
                {
                    changedPattern[n] = s;
                    n++;
                }
                h_length = changedPattern[0].Length;
            }
            else if (GAME_D == 2)
            {
                List<WordWithWeight> weightSort = new List<WordWithWeight>();
                for (int n = 0; n < WORD_LIST_C.Length; n++)
                {
                    WordWithWeight temp = new WordWithWeight(WORD_LIST_C[n]);
                    weightSort.Add(temp);
                }
                weightSort = weightSort.OrderByDescending(x => x.weight).ThenBy(x => x.weight).ToList();

                for (int n = 0; n < weightSort.Count; n++)
                {
                    changedPattern[n] = weightSort[n].word;
                }
                if (changedPattern.Length > 1)
                {
                    string ts = changedPattern[0];
                    changedPattern[0] = changedPattern[1];
                    changedPattern[1] = ts;
                }
                h_length = changedPattern[0].Length;
            }

            int limit = (crozzle_H - 1) * (crozzle_W - h_length + 1) * loopC;

            shouldStop = false;

            secondPassed = 0;
            secCounter.Enabled = true;

            secondPassedLabel.Invoke((MethodInvoker)delegate
            {
                secondPassedLabel.Text = "Total seconds passed: " + secondPassed;
            });

            for (int startX = 0; startX < crozzle_H - 1; startX++)
            {
                if (shouldStop) break;
                for (int startY = 0; startY < crozzle_W - h_length + 1; startY++)
                {
                    if (shouldStop) break;
                    for (int loop = 0; loop < loopC; loop++)
                    {
                        if (shouldStop) break;
                        data_Table_CreatedCrozzle = new DataTable();

                        NewCrozzle gameboard = new NewCrozzle(crozzle_W, crozzle_H, changedPattern);
                        gameboard.generateBoard(startX, startY, false);
                        crozzle_Array = gameboard.displayGrid();

                        int _cname = 0;
                        foreach (string t in crozzle_Array[0])
                        {
                            data_Table_CreatedCrozzle.Columns.Add(_cname.ToString(), typeof(string));
                            _cname++;
                        }

                        for (int i = 0; i < crozzle_H; i++)
                        {
                            data_Table_CreatedCrozzle.Rows.Add(crozzle_Array[i]);
                        }
                        Thread.Sleep(100);
                        crozzleDataGridView.Invoke((MethodInvoker)delegate
                        {
                            crozzleDataGridView.DataSource = data_Table_CreatedCrozzle;
                        });
                        Thread.Sleep(100);

                        int score = 0;
                        score = CrozzleListDataValidation();
                        if (score > bestpattern.score)
                        {
                            Console.WriteLine(score);
                            bestpattern.board = crozzle_Array;
                            for (int x = 0; x < crozzle_H; x++)
                            {
                                for (int y = 0; y < crozzle_W; y++)
                                {
                                    bestpattern.board[x][y] = crozzle_Array[x][y];
                                }
                            }
                            bestpattern.score = score;
                        }

                        crozzleProgressBar.BeginInvoke(
                            new MethodInvoker(() =>
                            {
                                int value = (startX) * (crozzle_W - h_length + 1) * loopC + (startY * loopC) + (loop + 1);
                                value = 50 * value;
                                value /= limit;
                                crozzleProgressBar.Value = value;
                            }
                            )
                        );
                        Thread.Sleep(100);
                    }
                }
            }

            limit = (crozzle_W - 1) * (crozzle_H - h_length + 1) * loopC;
            for (int startY = 0; startY < crozzle_W - 1; startY++)
            {
                if (shouldStop) break;
                for (int startX = 0; startX < crozzle_H - h_length + 1; startX++)
                {
                    if (shouldStop) break;
                    for (int loop = 0; loop < loopC; loop++)
                    {
                        if (shouldStop) break;
                        data_Table_CreatedCrozzle = new DataTable();

                        NewCrozzle gameboard = new NewCrozzle(crozzle_W, crozzle_H, WORD_LIST_C);
                        gameboard.generateBoard(startX, startY, true);
                        crozzle_Array = gameboard.displayGrid();

                        int _cname = 0;
                        foreach (string t in crozzle_Array[0])
                        {
                            data_Table_CreatedCrozzle.Columns.Add(_cname.ToString(), typeof(string));
                            _cname++;
                        }

                        for (int i = 0; i < crozzle_H; i++)
                        {
                            data_Table_CreatedCrozzle.Rows.Add(crozzle_Array[i]);
                        }
                        Thread.Sleep(100);
                        crozzleDataGridView.Invoke((MethodInvoker)delegate
                        {
                            crozzleDataGridView.DataSource = data_Table_CreatedCrozzle;
                        });
                        Thread.Sleep(100);

                        int score = 0;
                        score = CrozzleListDataValidation();
                        if (score > bestpattern.score)
                        {
                            Console.WriteLine(score);
                            bestpattern.board = crozzle_Array;
                            for (int x = 0; x < crozzle_H; x++)
                            {
                                for (int y = 0; y < crozzle_W; y++)
                                {
                                    bestpattern.board[x][y] = crozzle_Array[x][y];
                                }
                            }
                            bestpattern.score = score;
                        }

                        crozzleProgressBar.BeginInvoke(
                            new MethodInvoker(() =>
                            {
                                int value = (startY) * (crozzle_H - h_length + 1) * loopC + (startX * loopC) + (loop + 1);
                                value = 50 * value;
                                value /= limit;
                                crozzleProgressBar.Value = 50 + value;
                            }
                            )
                        );
                        Thread.Sleep(100);
                    }
                }
            }

            FinalSetUp();
            secCounter.Enabled = false;

            //as new crozzle has created
            Crozzle_Created = true;
            secondPassedLabel.Invoke((MethodInvoker)delegate
            {
                buttonSaveCrozzle.Visible = true;
            });
        }

        private void AfterFiveEvent(object sender, ElapsedEventArgs e)
        {
            secondPassed += 1;
            secondPassedLabel.Invoke((MethodInvoker)delegate
            {
                secondPassedLabel.Text = "Total seconds passed: " + secondPassed;
            });
            if (secondPassed > 290) shouldStop = true;
        }

        private void FinalSetUp()
        {
            string rowstr = "";
            for (int x = 0; x < crozzle_H; x++)
            {
                for (int y = 0; y < crozzle_W; y++)
                {
                    if (!string.Equals(bestpattern.board[x][y], "")) rowstr += bestpattern.board[x][y] + " ";
                    else rowstr += "  ";
                }
                rowstr += "\n";
            }
            Console.WriteLine(rowstr);
            Console.WriteLine(bestpattern.score);

            data_Table_CreatedCrozzle = new DataTable();

            int _cname = 0;
            foreach (string t in bestpattern.board[0])
            {
                data_Table_CreatedCrozzle.Columns.Add(_cname.ToString(), typeof(string));
                _cname++;
            }

            for (int i = 0; i < crozzle_H; i++)
            {
                data_Table_CreatedCrozzle.Rows.Add(bestpattern.board[i]);
            }
            Thread.Sleep(100);
            crozzleDataGridView.Invoke((MethodInvoker)delegate
            {
                crozzleDataGridView.DataSource = data_Table_CreatedCrozzle;
            });
            Thread.Sleep(100);

            this.Invoke((MethodInvoker)delegate
            {
                crozzleProgressBar.Visible = false;
                secondPassedLabel.Visible = false;
                pointLabel.Visible = true;
                crozzleDataGridView.Visible = true;
                createNewCrozzle.Enabled = true;
                buttonLoadWordList.Enabled = true;
                buttonLoadCrozzleList.Enabled = true;
                openWordListMenuItem.Enabled = true;
                openCrozzleMenuItem.Enabled = true;
                createCrozzleMenuItem.Enabled = true;
            });

            CrozzleListDataValidation();

            crozzle_W = crozzle_W ^ crozzle_H;
            crozzle_H = crozzle_W ^ crozzle_H;
            crozzle_W = crozzle_W ^ crozzle_H;
        }

        public class PatternDatabase
        {
            public string[][] board;
            public int score;
        }
    }
    public class WordWithWeight
    {
        public string word;
        public int weight;

        public WordWithWeight(string wr)
        {
            this.word = wr;
            this.weight = GetWordWeightInHardLevel(this.word);
        }

        private static int GetWordWeightInHardLevel(string word)
        {
            int result = 0;
            for (int i = 0; i < word.Length; i++)
            {
                result += GetCharWeight(word.ElementAt(i));
            }
            return result;
        }

        private static int GetCharWeight(char p)
        {
            if      (p == 'A' || p == 'E' || p == 'I' || p == 'O' || p == 'U') return 1;
            else if (p == 'B' || p == 'C' || p == 'D' || p == 'F' || p == 'G') return 2;
            else if (p == 'H' || p == 'J' || p == 'K' || p == 'L' || p == 'M') return 4;
            else if (p == 'N' || p == 'P' || p == 'Q' || p == 'R') return 8;
            else if (p == 'S' || p == 'T' || p == 'V') return 16;
            else if (p == 'W' || p == 'X' || p == 'Y') return 32;
            else if (p == 'Z') return 64;

            return 0;
        }
    }
}
