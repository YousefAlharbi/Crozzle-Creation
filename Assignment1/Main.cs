using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Main : Form
    {
        BackgroundWorker bw;

        // possible errot list in word loading from csv file 
        public string[] w_ERRORx = {   "First Column header does not contain any value." + Environment.NewLine,
                                "First Column header does not contain any numeric value." + Environment.NewLine,
                                "Second Column header does not contain any value." + Environment.NewLine,
                                "Second Column header does not contain any numeric value." + Environment.NewLine,
                                "Third Column header does not contain any value." + Environment.NewLine,
                                "Third Column header does not contain any numeric value." + Environment.NewLine,
                                "Fourth Column header does not contain value." + Environment.NewLine,
                                @" is not valid value for Fourth Column header. Valid values are either ""Easy"" or ""Hard""." + Environment.NewLine,
                                " cell contains nothing. But in the wordlist, there should be some data." + Environment.NewLine,
                                "Total number of given words are less then mentioned number of words in the header." + Environment.NewLine,
                                "Total number of given words are greater then mentioned number of words in the header." + Environment.NewLine,
                                " cell should be blank. But it contains some junk values." + Environment.NewLine};
        // error log string, it stores only header type errors
        public string ERROR_LOG_H;
        // error log string, it stores only word list invalidation type errors
        public string ERROR_LOG_W;
        // count the number of error in header
        public int ERROR_COUNT_H;
        //count the number of error in wordlist of csv file
        public int ERROR_COUNT_W;
        // indicates that, if any kind error happened or not
        public bool ERROR;

        // grid view cell style. for beautifying the GUI
        public DataGridViewCellStyle ERROR_STYLE;

        // error log string, it stores crozzle file errors
        public string ERROR_LOG_CROZZLE;
        // it counts the number of errors in crozzle file
        public int ERROR_COUNT_CROZZLE;
        // indicates that, if any kind error in crozzle happened or not
        public bool ERRORZ_CROZZLE;

        //file name saving
        string FILE_NAME;

        // grid view cell style. for beautifying the GUI
        public DataGridViewCellStyle ALPHABATE_STYLE;

        // grid view cell style. for beautifying the GUI. when duplicate words are found, this style eill indicate that
        public DataGridViewCellStyle DUPLICATION_STYLE;
        // grid view cell style. for beautifying the GUI. when invalid words are found, this style eill indicate that
        public DataGridViewCellStyle INVALID_STYLE;
        // grid view cell style. for beautifying the GUI. when constraint voilated words are found, this style eill indicate that
        public DataGridViewCellStyle CONSTRAINT_VIOLATION_STYLE;

        //data table for both word list and crozzle
        DataTable data_Table_WordList, data_Table_Crozzle, data_Table_CreatedCrozzle;

        //header record from preciously saved word list file
        public int WORD_NUM, crozzle_W, crozzle_H, GAME_D;

        //containing word list from previously loaded csv file
        public List<Dictionary> WORD_LIST;

        //holda the crozzle letters by it's 2 dimensional array.
        public string[][] CROZZLE2D_ALPHABATES;

        // game points calculation
        int GAME_POINTS;

        //For Creating Crozzle
        public string[] WORD_LIST_C;
        public string[][] crozzle_Array;
        public bool Crozzle_Created;

        public Main()
        {
            InitializeComponent();

            // Set the initially unneccessary items invisible
            wordDataGridView.Visible = false;
            crozzleDataGridView.Visible = false;
            pointLabel.Visible = false;
            crozzleProgressBar.Visible = false;
            //openCrozzleMenuItem.Enabled = false;
            //buttonLoadCrozzleList.Visible = false;
            progressBar.Visible = false;
            secondPassedLabel.Visible = false;
            Crozzle_Created = false;
            buttonSaveCrozzle.Visible = false;

            //declare the alphabate style of  grid cell for future use in crozzle grid view
            ERROR_STYLE = new DataGridViewCellStyle();
            ERROR_STYLE.BackColor = Color.Red;
            ERROR_STYLE.ForeColor = Color.White;

            //declare the error style of  grid cell for future use in  word grid view
            ALPHABATE_STYLE = new DataGridViewCellStyle();
            ALPHABATE_STYLE.BackColor = Color.Green;
            ALPHABATE_STYLE.ForeColor = Color.White;

            //declare the error style of  grid cell for future use in  crozzle grid view when duplicate words found
            DUPLICATION_STYLE = new DataGridViewCellStyle();
            DUPLICATION_STYLE.BackColor = Color.Blue;
            DUPLICATION_STYLE.ForeColor = Color.White;

            //declare the error style of  grid cell for future use in  crozzle grid view when invalid words found
            INVALID_STYLE = new DataGridViewCellStyle();
            INVALID_STYLE.BackColor = Color.Brown;
            INVALID_STYLE.ForeColor = Color.White;

            //declare the error style of  grid cell for future use in  crozzle grid view when constraint voilated word found
            CONSTRAINT_VIOLATION_STYLE = new DataGridViewCellStyle();
            CONSTRAINT_VIOLATION_STYLE.BackColor = Color.Yellow;
            CONSTRAINT_VIOLATION_STYLE.ForeColor = Color.Black;

            //create new object of Data_Table
            data_Table_WordList = new DataTable();
            data_Table_Crozzle = new DataTable();
            data_Table_CreatedCrozzle = new DataTable();

            // word list, from CSV file rows
            WORD_LIST = new List<Dictionary>();

            // Create a 1 seconds timer 
            secCounter = new System.Timers.Timer(1000);
            secCounter.Elapsed += new ElapsedEventHandler(AfterFiveEvent);
        }

        //next task.
        //implement for hard crozzle 1 day
        //make the problem automatic 1 day
        //save in csv format 1 day
        //29,30,1,2

        private void buttonLoadWordList_MouseHover(object sender, EventArgs e)
        {
            //change button design on Mouse hover
            buttonLoadWordList.BackColor = Color.LightSteelBlue;
            buttonLoadWordList.ForeColor = Color.White;
        }

        private void buttonLoadWordList_MouseLeave(object sender, EventArgs e)
        {
            //change button design to default when mouse leaves the button
            buttonLoadWordList.BackColor = Color.CornflowerBlue;
            buttonLoadWordList.ForeColor = Color.Black;
        }

        private void buttonLoadCrozzleList_MouseHover(object sender, EventArgs e)
        {
            //change button design on Mouse hover
            buttonLoadCrozzleList.BackColor = Color.LightSteelBlue;
            buttonLoadCrozzleList.ForeColor = Color.White;
        }

        private void buttonLoadCrozzleList_MouseLeave(object sender, EventArgs e)
        {
            //change button design to default when mouse leaves the button
            buttonLoadCrozzleList.BackColor = Color.CornflowerBlue;
            buttonLoadCrozzleList.ForeColor = Color.Black;
        }

        private void createNewCrozzle_MouseHover(object sender, EventArgs e)
        {
            //change button design on Mouse hover
            createNewCrozzle.BackColor = Color.LightSteelBlue;
            createNewCrozzle.ForeColor = Color.White;
        }

        private void createNewCrozzle_MouseLeave(object sender, EventArgs e)
        {
            //change button design to default when mouse leaves the button
            createNewCrozzle.BackColor = Color.CornflowerBlue;
            createNewCrozzle.ForeColor = Color.Black;
        }

        private void buttonSaveCrozzle_MouseHover(object sender, EventArgs e)
        {
            //change button design on Mouse hover
            buttonSaveCrozzle.BackColor = Color.LightSteelBlue;
            buttonSaveCrozzle.ForeColor = Color.White;
        }

        private void buttonSaveCrozzle_MouseLeave(object sender, EventArgs e)
        {
            //change button design to default when mouse leaves the button
            buttonSaveCrozzle.BackColor = Color.CornflowerBlue;
            buttonSaveCrozzle.ForeColor = Color.Black;
        }

        private void buttonResultCalculation_MouseHover(object sender, EventArgs e)
        {
            //change button design on Mouse hover
            buttonLoadCrozzleList.BackColor = Color.LightSteelBlue;
            buttonLoadCrozzleList.ForeColor = Color.White;
        }

        private void buttonResultCalculation_MouseLeave(object sender, EventArgs e)
        {
            //change button design to default when mouse leaves the button
            buttonLoadCrozzleList.BackColor = Color.CornflowerBlue;
            buttonLoadCrozzleList.ForeColor = Color.Black;
        }

        private void buttonResultCalculation_Click(object sender, EventArgs e)
        {

        }

        private void buttonLoadWordList_Click(object sender, EventArgs e)
        {
            LoadFile(1);
        }

        private void openWordListMenuItem_Click(object sender, EventArgs e)
        {
            LoadFile(1);
        }

        private void openCrozzleMenuItem_Click(object sender, EventArgs e)
        {
            if(crozzle_H > 0 && crozzle_W > 0 && WORD_LIST.Count > 0)
            {
                LoadFile(2);
            }
            else
            {
                MessageBox.Show("You have not loaded a valid word list yet.", "Operation can not be completed!!!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonLoadCrozzleList_Click(object sender, EventArgs e)
        {
            if (crozzle_H > 0 && crozzle_W > 0 && WORD_LIST.Count > 0)
            {
                LoadFile(2);
            }
            else
            {
                MessageBox.Show("You have not loaded a valid word list yet.", "Operation can not be completed!!!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createNewCrozzle_Click(object sender, EventArgs e)
        {
            if (crozzle_H > 0 && crozzle_W > 0 && WORD_LIST.Count > 0)
            {
                CreateNewCrozzle();
            }
            else
            {
                MessageBox.Show("You have not loaded a valid word list yet.", "Operation can not be completed!!!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createCrozzleMenuItem_Click(object sender, EventArgs e)
        {
            if (crozzle_H > 0 && crozzle_W > 0 && WORD_LIST.Count > 0)
            {
                //as new crozzle is creating, old made crozzle will vanish
                Crozzle_Created = false;
                buttonSaveCrozzle.Visible = false;

                CreateNewCrozzle();
            }
            else
            {
                MessageBox.Show("You have not loaded a valid word list yet.", "Operation can not be completed!!!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveCrozzle_Click(object sender, EventArgs e)
        {
            if (Crozzle_Created)
            {
                // Displays a SaveFileDialog so the user can save the csv
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "CSV File|*.csv";
                saveFileDialog1.Title = "Save a CSV File";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    //Console.WriteLine(saveFileDialog1.FileName);
                    writeCSV(crozzleDataGridView, saveFileDialog1.FileName);
                }
            }
            else
            {
                MessageBox.Show("No Crozzle has currently been made.", "Operation can not be completed!!!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CreateNewCrozzle()
        {
            //FOR TESTING
            crozzleProgressBar.Visible = true;
            secondPassedLabel.Visible = true;
            pointLabel.Visible = false;
            crozzleDataGridView.Visible = false;
            createNewCrozzle.Enabled = false;
            buttonLoadWordList.Enabled = false;
            buttonLoadCrozzleList.Enabled = false;
            openWordListMenuItem.Enabled = false;
            openCrozzleMenuItem.Enabled = false;
            createCrozzleMenuItem.Enabled = false;
            /*crozzleProgressBar.Visible = false;
            pointLabel.Visible = true;
            crozzleDataGridView.Visible = true;*/

            WORD_LIST_C = new string[WORD_LIST.Count];
            for (int i = 0; i < WORD_LIST.Count; i++)
            {
                WORD_LIST_C[i] = WORD_LIST[i].word;
            }

            CreateAndShowCrozzle();
            crozzleDataGridView.AutoResizeColumns();
            crozzleDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void LoadFile(int type)
        {
            // filters the file in file dialog for selecting *.csv format
            openFileDialog.Filter = "CSV Files(*.csv)|*.csv|All Files(*.*)|*.*";

            //show the file dialog
            DialogResult result = openFileDialog.ShowDialog();

            //after getting the input from filedialog, check if it's was successful selection of file.
            if (result == DialogResult.OK) 
            {
                //take the file name string's length
                int length = openFileDialog.FileName.ToString().Length;

                //take the file name
                string filename = openFileDialog.FileName;

                //used for log saving purpose
                FILE_NAME = openFileDialog.FileName;

                //compare if the inputed file is *.csv format or not
                if (filename.Substring(length-3) == "csv")
                {
                    try
                    {
                        //declare background worker . it will help us to not freeze the screen while computing the file data.
                        bw = new BackgroundWorker();
                        bw.WorkerSupportsCancellation = true;
                        bw.WorkerReportsProgress = true;

                        //declaring background job
                        bw.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
                        {
                            //getting data from csv file
                            GetDataTableFromCsv(filename, type);

                            this.Invoke((MethodInvoker)delegate
                            {
                                //type 1: means loading a file for wordlist
                                if (type == 1) messageLabel.Text = "Message: Word List File is validating. Please wait for a while.";
                                //type 2: means loading a file for crozzle list
                                else if (type == 2) messageLabel.Text = "Message: Crozzle File is validating. Please wait for a while.";
                            });

                            //check world list data validation
                            if (type == 1) WordListDataValidation();
                            else if (type == 2) CrozzleListDataValidation();

                            this.Invoke((MethodInvoker)delegate
                            {
                                //type 1: means loading a file for wordlist
                                //if (type == 1) wordDataGridView.Visible = true;
                                //type 2: means loading a file for crozzle list
                                if (type == 2)
                                {
                                    crozzleDataGridView.Visible = true;
                                    pointLabel.Visible = true;

                                    //as new crozzle is loading, made crozzle will vanish
                                    Crozzle_Created = false;
                                    buttonSaveCrozzle.Visible = false;
                                }
                            });
                        });

                        bw.ProgressChanged += new ProgressChangedEventHandler(
                        delegate(object o, ProgressChangedEventArgs args)
                        {
                        });

                        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                        delegate(object o, RunWorkerCompletedEventArgs args)
                        {
                            //type 1: means loading a file for wordlist
                            if (type == 1)
                            {
                                // data grid columns will resize it's size depending on the size of values inside them
                                //wordDataGridView.AutoResizeColumns();
                                //wordDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                                //wordDataGridView.FirstDisplayedCell = wordDataGridView.Rows[wordDataGridView.Rows.Count - 1].Cells[0];
                            }
                            //type 2: means loading a file for crozzle list
                            else if (type == 2)
                            {
                                // data grid columns will resize it's size depending on the size of values inside them
                                crozzleDataGridView.AutoResizeColumns();
                                crozzleDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                                //crozzleDataGridView.FirstDisplayedCell = crozzleDataGridView.Rows[crozzleDataGridView.Rows.Count - 1].Cells[0];
                            }

                            // enable the button, then was disabled for file uploading process.
                            openCrozzleMenuItem.Enabled = true;
                            buttonLoadCrozzleList.Enabled = true;
                            openWordListMenuItem.Enabled = true;
                            buttonLoadWordList.Enabled = true;
                            buttonLoadCrozzleList.Enabled = true;
                            createNewCrozzle.Enabled = true;
                            createCrozzleMenuItem.Enabled = true;

                            // invisible the temporary items that was visibled for file uploading process.
                            progressBar.Visible = false;
                            Console.WriteLine("Finished BackgroundWorker.");
                        });

                        //type 1: means loading a file for wordlist
                        if (type == 1)
                        {
                            //clear the data table
                            data_Table_WordList.Clear();

                            //set null as the data source as data grid view
                            wordDataGridView.DataSource = null;

                            //set data table as the data source as data grid view
                            wordDataGridView.DataSource = data_Table_WordList;

                            // show message in bottom of the screen
                            messageLabel.Text = "Message: Word List File is reading. Please wait for a while.";
                        }
                        //type 2: means loading a file for crozzle list
                        else if (type == 2)
                        {
                            //clear the data table
                            data_Table_Crozzle.Clear();

                            //set null as the data source as data grid view
                            crozzleDataGridView.DataSource = null;

                            //set data table as the data source as data grid view
                            crozzleDataGridView.DataSource = data_Table_Crozzle;

                            // show message in bottom of the screen
                            messageLabel.Text = "Message: Crozzle File is reading. Please wait for a while.";
                        }

                        //disabled menu items related to opening file
                        openWordListMenuItem.Enabled = false;
                        buttonLoadWordList.Enabled = false;
                        openCrozzleMenuItem.Enabled = false;
                        buttonLoadCrozzleList.Enabled = false;
                        buttonLoadCrozzleList.Enabled = false;
                        createNewCrozzle.Enabled = false;
                        createCrozzleMenuItem.Enabled = false;
                        //if(type == 1)wordDataGridView.Visible = false;
                        if (type == 2)
                        {
                            crozzleDataGridView.Visible = false;
                            pointLabel.Visible = false;
                        }
                        progressBar.Value = 0;
                        progressBar.Visible = true;

                        //clear word list data
                        if (type == 1) WORD_LIST.Clear();

                        //start background working
                        bw.RunWorkerAsync();
                    }
                    catch (IOException)
                    {
                    }
                }
                else
                {
                    MessageBox.Show("Please import only *.csv file.", "Wrong Type of file imported!!!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
