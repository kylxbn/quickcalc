using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuickCalc
{
    public partial class frmMain : Form
    {
        List<double> stack = new List<double>();
        bool nonNumberEntered, backspaceEntered;
        int inBase = 10, outBase = 10;


        public frmMain()
        {
            InitializeComponent();
        }

        private void input_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            opbg.BackColor = Color.Green;
            op.Text = "Ready";
            input.Text = "";
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (stack.Count >= 4)
            {
                if (outBase == 10)
                    s4.Text = stack[3].ToString("G10");
                else
                    s4.Text = ((int)(stack[3])).ToString("X");
            }
            else
                s4.Text = "";
            if (stack.Count >= 3)
            {
                if (outBase == 10)
                    s3.Text = stack[2].ToString("G10");
                else
                    s3.Text = ((int)(stack[2])).ToString("X");
            }
            else
                s3.Text = "";
            if (stack.Count >= 2)
            {
                if (outBase == 10)
                    s2.Text = stack[1].ToString("G10");
                else
                    s2.Text = ((int)(stack[1])).ToString("X");
            }
            else
                s2.Text = "";
            if (stack.Count >= 1)
            {
                if (outBase == 10)
                    s1.Text = stack[0].ToString("G10");
                else
                    s1.Text = (((int)(stack[0])).ToString("X"));
            }
            else
                s1.Text = "";
        }

        private void InsertNumber()
        {
            try
            {
                if (input.Text.Length == 0)
                {
                    stack.Insert(0, stack[0]);
                    opbg.BackColor = Color.Green;
                    op.Text = "Ready";
                    last.Text = "DUPLICATE";

                }
                else
                {
                    if (inBase == 10)
                    {
                        stack.Insert(0, Convert.ToDouble(input.Text));
                    }
                    else
                    {
                        stack.Insert(0, Convert.ToInt32(input.Text, inBase));
                    }
                    opbg.BackColor = Color.Green;
                    op.Text = "Ready";
                    last.Text = input.Text + " ENTER";
                }
                input.Text = "";
                UpdateDisplay();
            }
            catch
            {
                opbg.BackColor = Color.Red;
                op.Text = "Invalid input";
            }
        }

        private void keyboardMappingReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new frmReference();
            f.Show();
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new frmAbout();
            f.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (input.Text.Length > 0) InsertNumber();
            if (stack.Count > 0)
                Clipboard.SetText(stack[0].ToString());
            Application.Exit();
        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(((int)e.KeyChar).ToString());
            if (nonNumberEntered == true)
            {
                if (e.KeyChar == 13)
                {
                    InsertNumber();
                }
                else if (e.KeyChar == '+')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {
                        last.Text = stack[1].ToString("G8") + " " + stack[0].ToString("G8") + " +";
                        stack.Insert(0, stack[1] + stack[0]);
                        stack.RemoveAt(1);
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == '-')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {
                        last.Text = stack[1].ToString("G8") + " " + stack[0].ToString("G8") + " -";
                        stack.Insert(0, stack[1] - stack[0]);
                        stack.RemoveAt(1);
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == '*')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {
                        last.Text = stack[1].ToString("G8") + " " + stack[0].ToString("G8") + " *";
                        stack.Insert(0, stack[1] * stack[0]);
                        stack.RemoveAt(1);
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == '/')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {
                        if (stack[0] != 0)
                        {
                            last.Text = stack[1].ToString("G8") + " " + stack[0].ToString("G8") + " /";
                            stack.Insert(0, stack[1] / stack[0]);
                            stack.RemoveAt(1);
                            stack.RemoveAt(1);
                            opbg.BackColor = Color.Green;
                            op.Text = "Ready";
                            UpdateDisplay();
                        }
                        else
                        {
                            opbg.BackColor = Color.Red;
                            op.Text = "Can't divide by zero";
                        }
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == '\\')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {

                        stack.Insert(0, stack[1]);
                        stack.RemoveAt(2);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        last.Text = "SWAP";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'n')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = stack[0].ToString("G8") + " NEGATIVE";
                        stack.Insert(0, -stack[0]);
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'q')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        if (stack[0] >= 0)
                        {
                            last.Text = stack[0].ToString("G8") + " SQRT";
                            stack.Insert(0, Math.Sqrt(stack[0]));
                            stack.RemoveAt(1);
                            opbg.BackColor = Color.Green;
                            op.Text = "Ready";
                            UpdateDisplay();
                        }
                        else
                        {
                            opbg.BackColor = Color.Red;
                            op.Text = "Positive numbers only";
                        }
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'd')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {
                        last.Text = stack[0].ToString("G8") + "PERCENT " + stack[0].ToString("G8") + " *";
                        stack.Insert(0, stack[1] * (stack[0]/100));
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'D')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {
                        last.Text = stack[0].ToString("G8") + "PERCENT " + stack[0].ToString("G8") + " *";
                        stack.Insert(0, stack[1] * (stack[0] / 100));
                        stack.RemoveAt(1);
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'X')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count > 0)
                        Clipboard.SetText(stack[0].ToString());
                    Application.Exit();
                }
                else if (e.KeyChar == '!')
                {
                    inBase = 10;
                    opbg.BackColor = Color.Green;
                    txtInputBase.Text = "DEC";
                    op.Text = "Ready";
                }
                else if (e.KeyChar == '@')
                {
                    inBase = 2;
                    opbg.BackColor = Color.Green;
                    txtInputBase.Text = "BIN";
                    op.Text = "Ready";
                }
                else if (e.KeyChar == '#')
                {
                    inBase = 8;
                    opbg.BackColor = Color.Green;
                    txtInputBase.Text = "OCT";
                    op.Text = "Ready";
                }
                else if (e.KeyChar == '$')
                {
                    inBase = 16;
                    opbg.BackColor = Color.Green;
                    txtInputBase.Text = "HEX";
                    op.Text = "Ready";
                }
                else if (e.KeyChar == '1')
                {
                    outBase = 10;
                    opbg.BackColor = Color.Green;
                    op.Text = "Ready";
                    txtOutputBase.Text = "DEC";
                    UpdateDisplay();
                }
                else if (e.KeyChar == '4')
                {
                    outBase = 16;
                    opbg.BackColor = Color.Green;
                    op.Text = "Ready";
                    txtOutputBase.Text = "HEX";
                    UpdateDisplay();
                }
                else if (e.KeyChar == 'r')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {
                        last.Text = stack[1].ToString("G8") + " " + stack[0].ToString("G8") + " ROOT";
                        stack.Insert(0, Math.Pow(stack[1], 1d / stack[0]));
                        stack.RemoveAt(1);
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'p')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {
                        last.Text = stack[1].ToString("G8") + " " + stack[0].ToString("G8") + " POW";
                        stack.Insert(0, Math.Pow(stack[1], stack[0]));
                        stack.RemoveAt(1);
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 's')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = stack[1].ToString("G8") + " SIN";
                        stack.Insert(0, Math.Sin(stack[0]));
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'c')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = stack[1].ToString("G8") + " COS";
                        stack.Insert(0, Math.Cos(stack[0]));
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 't')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = stack[1].ToString("G8") + " TAN";
                        stack.Insert(0, Math.Tan(stack[0]));
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'S')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = stack[1].ToString("G8") + " ASIN";
                        stack.Insert(0, Math.Asin(stack[0]));
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'C')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = stack[1].ToString("G8") + " ACOS";
                        stack.Insert(0, Math.Acos(stack[0]));
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'T')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = stack[1].ToString("G8") + " ATAN";
                        stack.Insert(0, Math.Atan(stack[0]));
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'i')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = "1 " + stack[0].ToString("G8") + " /";
                        stack.Insert(0, 1d/stack[0]);
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'L')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = stack[1].ToString("G8") + " LN";
                        stack.Insert(0, Math.Log(stack[0]));
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'l')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 1)
                    {
                        last.Text = stack[1].ToString("G8") + " LOG";
                        stack.Insert(0, Math.Log10(stack[0]));
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'm')
                {
                    if (input.Text.Length > 0) InsertNumber();
                    if (stack.Count >= 2)
                    {
                        last.Text = stack[1].ToString("G8") + " " + stack[0] + " MOD";
                        stack.Insert(0, stack[1] % stack[0]);
                        stack.RemoveAt(1);
                        stack.RemoveAt(1);
                        opbg.BackColor = Color.Green;
                        op.Text = "Ready";
                        UpdateDisplay();
                    }
                    else
                    {
                        opbg.BackColor = Color.Red;
                        op.Text = "Too few items";
                    }
                }
                else if (e.KeyChar == 'P')
                {
                    last.Text = "PI ENTER";
                    stack.Insert(0, Math.PI);
                    opbg.BackColor = Color.Green;
                    op.Text = "Ready";
                    UpdateDisplay();
                }
                else if (e.KeyChar == 'E')
                {
                    last.Text = "E ENTER";
                    stack.Insert(0, Math.E);
                    opbg.BackColor = Color.Green;
                    op.Text = "Ready";
                    UpdateDisplay();
                }
            }
            else
            {
                if (input.Text.Length < 13 && !backspaceEntered)
                {
                    if (e.KeyChar >= 'a' && e.KeyChar <= 'f')
                        input.Text += e.KeyChar.ToString().ToUpper();
                    else
                        input.Text += e.KeyChar.ToString();
                }
                else if (backspaceEntered)
                    if (input.Text.Length == 0)
                    {
                        if (stack.Count == 0)
                        {
                            opbg.BackColor = Color.Red;
                            op.Text = "Nothing to delete";
                        }
                        else
                        {
                            stack.RemoveAt(0);
                            opbg.BackColor = Color.Green;
                            op.Text = "Ready";
                            last.Text = "DEL";
                            UpdateDisplay();
                        }
                    }
                    else
                        input.Text = input.Text.Substring(0, input.Text.Length - 1);
                
            }
            e.Handled = true;
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;
            backspaceEntered = false;

            if (e.KeyCode != Keys.Back && e.KeyCode != Keys.OemPeriod && e.KeyCode != Keys.Decimal)
            {
                if (inBase == 10 && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)) nonNumberEntered = true;
                else if (inBase == 2 && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad1)) nonNumberEntered = true;
                else if (inBase == 8 && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad7)) nonNumberEntered = true;
                else if (inBase == 16 && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) &&
                    (e.KeyCode < Keys.A || e.KeyCode > Keys.F)) nonNumberEntered = true;
            }
            if (e.KeyCode == Keys.Back)
                backspaceEntered = true;

            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }
    }
}
