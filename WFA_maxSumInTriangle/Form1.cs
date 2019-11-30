using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_maxSumInTriangle
{
    public partial class Form1 : Form
    {
        Methods methods = new Methods();

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            int lengthOfTriangle = 0;
            List<string[]> list = new List<string[]>();
            List<int[]> listOfIndexWithNumbers = new List<int[]>();
            DataTable table = new DataTable();
            int line = 0;
            List<int> indexOfColumn = new List<int>();
            bool validate = true;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            using (StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.Default))
                            {
                                while (sr.Peek() >= 0)
                                {
                                    string text = sr.ReadLine();                                    
                                    string[] lines = text.Split(' ');

                                    if(!methods.IsNumber0_100(lines) && validate)
                                    {                                        
                                        validate = false;
                                    }
                                    else
                                    {
                                        list.Add(lines);
                                        lengthOfTriangle = lines.Length;
                                    }
                                }                                
                            }
                        }

                        if (validate)
                        {
                            int[,] list1 = new int[lengthOfTriangle, lengthOfTriangle];
                            foreach (var array in list)
                            {
                                for (int j = 0; j < array.Length; j++)
                                {
                                    int number = 0;
                                    int.TryParse(array[j], out number);
                                    list1[line, j] = number;
                                }
                                line++;
                            }

                            int posSize = (int)Math.Pow(2, list1.GetLength(0) - 1);
                            int maxSum = 0;
                            int tempSum, index;
                            List<int> indexOfColumnTemp = new List<int>();
                            for (int i = 0; i <= posSize; i++)
                            {
                                tempSum = list1[0, 0];
                                indexOfColumnTemp.Clear();
                                indexOfColumnTemp.Add(lengthOfTriangle);
                                index = 0;
                                for (int j = 0; j < list1.GetLength(0) - 1; j++)
                                {
                                    index = index + (i >> j & 1);
                                    tempSum += list1[j + 1, index];
                                    indexOfColumnTemp.Add(index);
                                }
                                if (tempSum > maxSum)
                                {
                                    maxSum = tempSum;
                                    indexOfColumn = new List<int>(indexOfColumnTemp);
                                }
                            }
                            label1.Text = "Max sum = " + maxSum.ToString();

                            #region Fill grid values

                            int temp = (lengthOfTriangle * 2) / 2;
                            for (int i = 1; i < lengthOfTriangle * 2; i++)//add columns 
                            {
                                table.Columns.Add("column" + i, typeof(String));
                            }
                            DataRow dataRow = table.NewRow();

                            listOfIndexWithNumbers = methods.CalculateIndexWithValues(lengthOfTriangle * 2, list.Count);

                            for (int j = 0, k = 0; j < list.Count; j++)
                            {
                                dataRow = table.NewRow();
                                for (int i = 1; i < lengthOfTriangle * 2; i++)
                                {
                                    if (listOfIndexWithNumbers[j].Contains(i))
                                    {
                                        dataRow["column" + i] = list[j][k];
                                        k++;
                                    }
                                    else
                                        dataRow["column" + i] = "";//empty fields
                                }
                                table.Rows.Add(dataRow);
                                k = 0;
                            }

                            dataGridView1.DataSource = table;
                            methods.SetGrid(dataGridView1, listOfIndexWithNumbers, indexOfColumn);

                            #endregion
                        }
                        else
                            MessageBox.Show("Error: Illegal value");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

       
    }
}
