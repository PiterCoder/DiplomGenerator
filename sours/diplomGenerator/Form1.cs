using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace diplomGenerator
{
    public partial class Form1 : Form
    {
        string inksPath = "";
        string[] head;
        string[,] data;
        string fon = "";
        string outPath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                checkBox2.Checked = true;
                string[] tdata = File.ReadAllLines(openFileDialog1.FileName);
                head = tdata[0].Split('\t');
                data = new string[tdata.Length - 1, head.Length];
                for (int i = 1; i < tdata.Length; i++)
                {
                    string[] a = tdata[i].Split('\t');
                    for (int j = 0; j < head.Length; j++) {
                        data[i-1, j] = a[j];
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                checkBox3.Checked = true;
                fon = File.ReadAllText(openFileDialog2.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                checkBox4.Checked = true;
                outPath = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if((checkBox3.Checked == checkBox2.Checked)&&(checkBox4.Checked== checkBox3.Checked) && (checkBox3.Checked == true))
            {
                progressBar1.Maximum = data.Length / head.Length-1;
                for(int i =0;i< progressBar1.Maximum; i++)
                {
                    string tfon = new string(fon.ToCharArray());
                    string outName = "";
                    for(int j = 0; j < head.Length; j++)
                    {
                        progressBar1.Value = i;
                        if (head[j] == "!N!")
                        {
                            outName = data[i, j];
                            continue;
                        }
                        tfon = tfon.Replace(head[j],data[i,j]);

                    }
                    File.WriteAllText(outPath + '\\' + outName, tfon);
                    tfon = null;
                    GC.Collect();
                }
            }
            if (checkBox1.Checked&&checkBox4.Checked)
            {
                string[] list = Directory.GetFiles(outPath);
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i].IndexOf(".svg",list[i].Length-5)>0)
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = inksPath;
                        p.StartInfo.Arguments = "-f \""+list[i]+"\" -A \""+ list[i].Replace(".svg",".pdf")+"\"";
                        p.Start();

                    }
                }
            }
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            if(openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                checkBox1.Checked = true;
                inksPath = openFileDialog3.FileName;
            }
        }
    }
}
