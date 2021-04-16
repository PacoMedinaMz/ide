using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProyCompilador {
    public partial class ventana : Form {

        struct WPosition {
            public string Word;
            public int Position;
            public int Length;
            public override string ToString() {
                return "Word = " + Word + ", Position = " + Position + ", Length = " + Length + "\n";
            }
        };

        private RegexLexer lexer;
        private List<string> reservadas;
        private OpenFileDialog ofd;
        private SaveFileDialog sfd;


        public ventana() {
            InitializeComponent();

            lexer = new RegexLexer();
            reservadas = new List<string>() { "abstract", "as", "assert", "async", "await",
                "checked", "const", "continue", "default", "delegate", "base", "break", "case",
                "do", "else", "enum", "event", "explicit", "extern","extends", "false", "finally",
                "fixed", "for", "foreach", "goto", "if", "implicit","implements", "in", "interface",
                "internal", "is", "lock", "new", "null", "operator","catch",
                "out", "override", "params", "private", "protected", "public", "readonly",
                "ref", "return", "sealed", "sizeof", "stackalloc", "static",
                "switch", "this", "throw", "true", "try", "typeof", "namespace",
                "unchecked", "unsafe", "virtual", "void", "while", "float", "int", "long", "object",
                "get", "set", "new", "partial", "yield", "add", "remove", "value", "alias", "ascending",
                "descending", "from", "group", "into", "orderby", "select", "where",
                "join", "equals", "using","boolean", "byte", "char", "decimal", "double", "dynamic",
                "sbyte", "short", "string", "uint", "ulong", "ushort", "var", "class", "struct","sizeof"
                ,"LinkedList","import","final","package","instanceof","native",
                "strictfp","super","synchronized","throws","transient","volatile","main"};

            using (System.IO.StreamReader sr = new StreamReader(@"..\..\..\RegexLexer.cs")) {
                lexer.AddTokenRule(@"\s+", "Espacio", true);
                lexer.AddTokenRule(@"[\(\)\{\}\[\];,]", "Delimitador");
                lexer.AddTokenRule(@"\d*\.?\d+", "Numero");
                lexer.AddTokenRule("\".*?\"", "Cadena");
                lexer.AddTokenRule(@">|<|==|>=|<=|!", "Comparador");
                lexer.AddTokenRule(@"'\\.'|'[^\\]'", "Caracter");
                lexer.AddTokenRule(@"\b[_a-zA-Z][\w]*\b", "Identificador");
                lexer.AddTokenRule("/[*].*?[*]/", "Comentario1");
                lexer.AddTokenRule(@"[\.=\+\-/*%]", "Operador");
                lexer.AddTokenRule("//[^\r\n]*", "Comentario2");

                lexer.Compile(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                checkCode();
                txtCode.Focus();
            }
        }

        public void richTextBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Space) {//Tecla espacio
                txtCode.Text = "espacio";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {
            checkCode();
            txtCode.Focus();
            drawWords();
        }

        
        WPosition[] TheBuffer = new WPosition[4000];
        private int parseLine(string s) {
            TheBuffer.Initialize();
            int count = 0;
            Regex r = new Regex(@"\w+|[^A-Za-z0-9_ \f\t\v]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m;

            for (m = r.Match(s); m.Success; m = m.NextMatch()) {
                TheBuffer[count].Word = m.Value;
                TheBuffer[count].Position = m.Index;
                TheBuffer[count].Length = m.Length;
                count++;
            }

            return count;
        }

        private Color showColor(string s) {
            foreach (string word in reservadas) {
                if (s == word) {
                    return Color.Blue;
                }
            }
            return Color.Black;
        }

        private bool comment(string s) {
            string testString = s.Trim();
            return ((testString.Length >= 2) && (testString[0] == '/') && (testString[1] == '/'));
        }

        private void drawWords() {
            int Start = txtCode.SelectionStart;
            int Length = txtCode.SelectionLength;
            Color colorComentario = Color.LightGreen;

            int pos = Start;
            while ((pos > 0) && (txtCode.Text[pos - 1] != '\n'))
                pos--;

            int pos2 = Start;
            while ((pos2 < txtCode.Text.Length) &&
                    (txtCode.Text[pos2] != '\n'))
                pos2++;

            string s = txtCode.Text.Substring(pos, pos2 - pos);
            if (comment(s) == true) {
                txtCode.Select(pos, pos2 - pos);
                txtCode.SelectionColor = colorComentario;
            } else {
                string previousWord = "";
                int count = parseLine(s);
                for (int i = 0; i < count; i++) {
                    WPosition wp = TheBuffer[i];

                    if (wp.Word == "/" && previousWord == "/") {
                        int posCommentStart = wp.Position - 1;
                        int posCommentEnd = pos2;
                        while (wp.Word != "\n" && i < count) {
                            wp = TheBuffer[i];
                            i++;
                        }

                        i--;
                        posCommentEnd = pos2;
                        txtCode.Select(posCommentStart + pos, posCommentEnd - (posCommentStart + pos));
                        txtCode.SelectionColor = colorComentario;
                    } else {
                        Color c = showColor(wp.Word);
                        txtCode.Select(wp.Position + pos, wp.Length);
                        txtCode.SelectionColor = c;
                    }

                    previousWord = wp.Word;
                }
            }

            if (Start >= 0)
                txtCode.Select(Start, Length);
        }

        private void checkCode() {
            pLexico.Rows.Clear();
            int n = 0, e = 0;

            foreach (var tk in lexer.GetTokens(txtCode.Text)) {
                if (tk.Name == "Error") e++;

                pLexico.Rows.Add(tk.Name, tk.Lexema, tk.Index, tk.Linea, tk.Columna);
                n++;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFile();
        }

        private void openToolStripButton_Click(object sender, EventArgs e) {
            openFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            savefile();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e) {
            savefile();
        }

        private void openFile() {
            ofd = new OpenFileDialog();
            ofd.Filter = "Por favor, únicamente archivos con código legible|*.txt";
            ofd.Multiselect = false;

            if (ofd.ShowDialog().Equals(DialogResult.OK)) {
                txtCode.Text = new System.IO.StreamReader(ofd.FileName).ReadToEnd();
            }

            checkCode();
            txtCode.Focus();
            drawWords();
        }

        private void savefile() {
            sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.txt)|*.txt";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK) {
                using (StreamWriter writer = new StreamWriter(sfd.OpenFile())) {
                    writer.Write(txtCode.Text);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.txt)|*.txt";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK) {
                using (StreamWriter writer = new StreamWriter(sfd.OpenFile())) {
                    writer.Write(txtCode.Text);
                }
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e) {
            txtCode.Redo();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
            txtCode.Undo();
        }

        private void redo_Click(object sender, EventArgs e) {
            txtCode.Redo();
        }

        private void undo_Click(object sender, EventArgs e) {
            txtCode.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
            cut();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e) {
            cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            copy();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e) {
            copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            paste(((RichTextBox)this.txtCode).SelectionStart);
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e) {
            paste(((RichTextBox)this.txtCode).SelectionStart);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) {
            txtCode.SelectAll();
            txtCode.Focus();
        }

        private void paste(int position) {
            this.txtCode.Text = this.txtCode.Text.Insert(position, Clipboard.GetText());
        }

        private void cut() {
            if (txtCode.SelectedText != string.Empty) {
                Clipboard.SetData(DataFormats.Text, txtCode.SelectedText);
                txtCode.SelectedText = string.Empty;
            }
        }

        private void copy() {
            if (txtCode.SelectedText != string.Empty) {
                Clipboard.SetData(DataFormats.Text, txtCode.SelectedText);
            }
        }
    }
}
