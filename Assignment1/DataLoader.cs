using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Main
    {
        private void GetDataTableFromCsv(string path, int type)
        {
            //if (type == 2)
            //{
                //as new crozzle is loading, made crozzle will vanish
                Crozzle_Created = false;
                buttonSaveCrozzle.Invoke((MethodInvoker)delegate
                {
                    buttonSaveCrozzle.Visible = false;
                });
            //}

            //read file data
            string[] str = File.ReadAllLines(path);

            // header serial number
            int _cname = 0;

            //allocate data table variable
            if (type == 1) data_Table_WordList = new DataTable();
            else if (type == 2) data_Table_Crozzle = new DataTable();

            //split first line of csv file
            string[] temp = Split(str[0]);

            // add column in data table for each header found in csv file
            foreach (string t in temp)
            {
                if (type == 1) data_Table_WordList.Columns.Add(_cname.ToString(), typeof(string));
                else if (type == 2) data_Table_Crozzle.Columns.Add(_cname.ToString(), typeof(string));
                _cname++;
            }

            // read all other rows and split them and show them in screen
            for (int i = 0; i < str.Length; i++)
            {
                string[] t = Split(str[i]);

                // add rows on table. table choice is depending on which data type provided
                if (type == 1) data_Table_WordList.Rows.Add(t);
                else if (type == 2) data_Table_Crozzle.Rows.Add(t);

                //progress bar value chaging after each update
                float value = (float)i / (float)str.Length;

                if (type == 1)
                {
                    value *= 50;

                    // set new data table as datagridview's data source
                    wordDataGridView.Invoke((MethodInvoker)delegate
                    {
                        wordDataGridView.DataSource = data_Table_WordList;
                    });
                }
                else if (type == 2)
                {
                    value *= 50;

                    // set new data table as datagridview's data source
                    crozzleDataGridView.Invoke((MethodInvoker)delegate
                    {
                        crozzleDataGridView.DataSource = data_Table_Crozzle;
                    });
                }

                // set progressbar
                progressBar.BeginInvoke(new MethodInvoker(() => { progressBar.Value = (int)value; }));
                Console.WriteLine((int)value);

                // wait for 60 milisecond for UI/other thread to take place in execution to avoid UI freeze.
                Thread.Sleep(60);
            }
        }

        // splitting each row of the csv file using regular expression
        private string[] Split(string str)
        {
            StringCollection resultList = new StringCollection();
            try
            {
                //regular expression for splitting *.CSV formatted file's row strings into grid elemnts.
                Regex pattern = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");

                // match the regular expression at row positions and add the separeted values in result list
                foreach (Match m in pattern.Matches(str))
                {
                    resultList.Add(m.Value);
                }
                return resultList.Cast<string>().ToArray<string>();
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
