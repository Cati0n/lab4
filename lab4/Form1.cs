using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public matrix A;
        public matrix B;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = groupBox2.Enabled = groupBox3.Enabled = groupBox4.Enabled = false;
            toolStripLabel1.Text = toolStripLabel2.Text = "__________";
            toolStripSplitButton1.Enabled = false;
            listBox1.Enabled = listBox2.Enabled = false;
            listBox2.EnabledChanged += new EventHandler(listBox2_EnabledChanged);
        }
        
        public class matrix
        {
            public static int rang;
            public int[,] mtr;

            public matrix (int i)
            {
                rang = i;
                mtr = new int[i, i];
            }

            public int [,] generic()
            {
                Random elem = new Random();
                for (int i = 0; i < rang; i ++)
                {
                    for (int j = 0; j< rang; j ++)
                    {
                        mtr[i, j] = elem.Next(0, 10);
                    }
                }
                return mtr;
            }

            public string[] to_str(int[,] MR)
            {
                string[] str = new string[rang];
                for (int i = 0; i< rang; i++ )
                {
                    str[i] = "";
                    for (int j = 0; j< rang; j++)
                    {
                        str[i] += MR[i, j] + "   ";
                    }
                }
                return str;
            }
        }
        
        public class ind_matrix
        {
            int rang = matrix.rang;
            public int[] ind_matr = new int[matrix.rang];

            public int[] row_matr(int[,] C, int i)
            {
                for (int j = 0; j < rang; j++)
                {
                    ind_matr[j] = C[i, j];
                }
                return ind_matr;
            }

            public static ind_matrix operator ++ (ind_matrix MX)
            {
                for (int i = 0; i< matrix.rang; i++)
                {
                    MX.ind_matr[i] += MX.ind_matr[i];
                }
                return MX;
            }

            public static ind_matrix operator --(ind_matrix MX)
            {
                for (int i = 0; i < matrix.rang; i++)
                {
                    MX.ind_matr[i] -= MX.ind_matr[i];
                }
                return MX;
            }

            public static bool operator ==(ind_matrix MX1, ind_matrix MX2)
            {
                for (int i =0; i<matrix.rang; i ++)
                {
                    if (MX1.ind_matr[i] != MX2.ind_matr[i])
                        return false;
                }
                return true; 
            }

            public static bool operator !=(ind_matrix MX1, ind_matrix MX2)
            {
                return !(MX1 == MX2);
            }
            

            public override bool Equals(object obj)
            {
                return (obj is ind_matrix) && (this == (ind_matrix)obj);
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }
        }



        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            if (domainUpDown1.SelectedIndex == -1)
            {
                MessageBox.Show("Size of matrix is 3x3. \n You can change size by yourself.");
                domainUpDown1.SelectedIndex = 0;
            }

            switch (domainUpDown1.SelectedIndex)
            {
                case 0:
                    {
                        matrix.rang = 3;
                        break;
                    }

                case 1:
                    {
                        matrix.rang = 4;
                        break;
                    }

                case 2:
                    {
                        matrix.rang = 5;
                        break;
                    }
            }
        }



        private void GenericA_Click(object sender, EventArgs e)
        {
            listBox1.Enabled = true;
            listBox1.Items.Clear();

            A = new matrix(matrix.rang);
           string[] str_A = A.to_str(A.generic());

            for (int i = 0; i < matrix.rang; i++)
                listBox1.Items.Add(str_A[i]);
        }

        private void GenericB_Click(object sender, EventArgs e)
        {
            listBox2.Enabled = true;
            listBox2.Items.Clear();

            B = new matrix(matrix.rang);
            string [] str_B = B.to_str(B.generic());

            for (int i = 0; i < matrix.rang; i++)
                listBox2.Items.Add(str_B[i]);
        }

        private void listBox2_EnabledChanged(object sender, EventArgs e)
        {
            if (listBox1.Enabled == true && listBox2.Enabled == true)
            {
                groupBox1.Enabled = groupBox2.Enabled = groupBox3.Enabled = groupBox4.Enabled = true;
                toolStripSplitButton1.Enabled = true;
            }
        }

        //private void listBox_EnabledChanged(object sender, EventArgs e)
        //{
        //    if (listBox1.Enabled == true && listBox2.Enabled == true)
        //    {
        //        groupBox1.Enabled = groupBox2.Enabled = groupBox3.Enabled = groupBox4.Enabled = true;
        //        toolStripSplitButton1.Enabled = true;
        //    }
        //}

        private void dubbleRow_Click(object sender, EventArgs e)
        {
            toolStripLabel1.Text = toolStripLabel2.Text = "row";
            toolStripButton1.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int ind_row_A = Convert.ToInt16(toolStripTextBox1.Text) - 1;
            int ind_row_B = Convert.ToInt16(toolStripTextBox2.Text) - 1;

            for (int j = 0; j < matrix.rang; j++)
            {
                B.mtr[ind_row_B, j] = A.mtr[ind_row_A, j];
                listBox2.Items.Clear();
                string[] str_B = B.to_str(B.mtr);
                for (int i = 0; i < matrix.rang; i++)
                    listBox2.Items.Add(str_B[i]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int row = (int)numericUpDown6.Value - 1;
            ind_matrix IM = new ind_matrix();
            IM.ind_matr = IM.row_matr(A.mtr, row);
            IM++;
            string s = "";
            for (int i = 0; i < matrix.rang; i++)
                s += IM.ind_matr[i] + " ";
            textBox2.Text = s;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ind_matrix row_matrA = new ind_matrix();
            int rowA = (int)numericUpDown7.Value - 1;
            ind_matrix row_matrB = new ind_matrix();
            int rowB = (int)numericUpDown5.Value - 1;
            row_matrA.ind_matr = row_matrA.row_matr(A.mtr, rowA);
            row_matrB.ind_matr = row_matrB.row_matr(B.mtr, rowB);

            if (row_matrA == row_matrB) MessageBox.Show("Rows are equal");
            else MessageBox.Show("Rows are different");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ind_matrix rowMatrixA = new ind_matrix();
            //int rowA = (int)numericUpDown1.Value - 1;
            //ind_matrix rowMatrixB = new ind_matrix();
            //int rowB = (int)numericUpDown2.Value - 1;
            //rowMatrixA.ind_matr = rowMatrixA.row_matr(A.mtr, rowA);
            //rowMatrixB.ind_matr = rowMatrixB.row_matr(B.mtr, rowB);
            //int sum;

            //listBox3.Items.Add(sum);
            ///////////////////////////////
            //int indexA = A.mtr.GetLength(0);


            int rowMatrixA = Convert.ToInt32(numericUpDown1.Value);
            int rowMatrixB = Convert.ToInt32(numericUpDown2.Value);
            int sum;
            if ((rowMatrixA >= A.mtr.GetLength(0)) || (rowMatrixB >= B.mtr.GetLength(0)))
            {
                MessageBox.Show("Title");
            }
            else
            {
                for (int i = 0; i < (rowMatrixA+1); i++)
                {
                    sum = A.mtr[rowMatrixA, i] + B.mtr[rowMatrixB, i];
                    listBox3.Items.Add(sum);
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
        }
    }
}
