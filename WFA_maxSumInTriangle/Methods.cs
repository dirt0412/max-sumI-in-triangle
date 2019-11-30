using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_maxSumInTriangle
{
    public class Methods
    {
        public List<int[]> CalculateIndexWithValues(int size, int level)
        {
            List<int[]> arrayTemp = new List<int[]>();
            int t = size / 2;
            for (int i = 0; i < level; i++)
            {
                int[] arr = new int[i + 1];

                if (i == 0)
                    arr[i] = t;
                else
                    for (int j = 0; j < arr.Length - 1; j++)
                    {
                        arr[j] = arrayTemp.Last()[j] - 1;
                        arr[j + 1] = arrayTemp.Last()[j] + 1;
                    }
                arrayTemp.Add(arr);
            }
            return arrayTemp;
        }

        public void SetGrid(DataGridView dgv, List<int[]> listOfIndexWithNumbers, List<int> indexOfColumn)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].Width = 60;
            }

            for (int j = 0; j < dgv.RowCount; j++)
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    if (!string.IsNullOrEmpty(dgv.Rows[j].Cells[i].Value.ToString()))
                        dgv.Rows[j].Cells[i].Style.BackColor = Color.LightGray;

                    if (j == 0 && !string.IsNullOrEmpty(dgv.Rows[j].Cells[i].Value.ToString()) && i + 1 == listOfIndexWithNumbers[j][0])
                        dgv.Rows[j].Cells[i].Style.BackColor = Color.Red;
                    else
                    if (j != 0 && !string.IsNullOrEmpty(dgv.Rows[j].Cells[i].Value.ToString()) && i + 1 == listOfIndexWithNumbers[j][indexOfColumn[j]])
                    {
                        dgv.Rows[j].Cells[i].Style.BackColor = Color.Red;
                    }
                }
        }
        public bool IsNumber0_100(string[] lines)
        {
            int d;
            foreach (var number in lines)
            {
                if (!(int.TryParse(number, out d) && d <= 100 && d >= 0))
                    return false;
            }
            return true;
        }
    }
}
