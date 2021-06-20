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

        //--- Sintáctico ---

        //--- Sintáctico ---


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
                    if ((wp.Word == "/" && previousWord == "/") || (previousWord == "/" && wp.Word == "*")) {
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
                if (tk.Name == "Error")
                    e++;

                pLexico.Rows.Add(tk.Name, tk.Lexema, tk.Index, tk.Linea, tk.Columna);
                n++;
            }
        }




        private void btnCompilar_Click_1(object sender, EventArgs e) {
            txtLog.Text = "";
            pSintactico.Text = "";
            string sourceCode = txtCode.Text;

            var tokens = Scanner(Splitter(sourceCode));
            foreach (var s in tokens) {
                pSintactico.Text += s.name + "\t" + s.type + "\r\n";
            }
            pSintactico.Text += "\r\n";

            var errors = SemanticAnalyzer(tokens);
            foreach (var s in errors) {
                txtLog.Text += s + "\r\n";
            }
        }


        List<string> Splitter(string sourceCode) {
            List<string> splitSourceCode = new List<string>();
            Regex RE = new Regex(@"\d+\.\d+|\'.*\'|\w+|\(|\)|\++|-+|\*|%|,|;|&+|\|+|<=|<|>=|>|==|=|!=|!|\{|\}|\/");
            foreach (Match m in RE.Matches(sourceCode)) {
                splitSourceCode.Add(m.Value);
            }
            return splitSourceCode;
        }

        class Token {
            public string name = "";
            public string type = "";

            public Token() {
            }

            public Token(string name, string type) {
                this.name = name;
                this.type = type;
            }
        }
        class variable {
            public string name;
            public int integer;
            public float floaat;
            public string str;
            public double doubl;
            public bool bol;
            public char charachter;
        }
        int f = 0;
        int ct = 0;
        int fstop = 0;
        string prevvar;
        string prevop;
        string firstvar;
        int flagoutput = 0;
        int number = 0;
        int number2 = 0;
        string prevtype;
        string prevtypevar;
        string vartype;
        List<variable> Lvar = new List<variable>();
        List<string> varsize = new List<string>();

        List<Token> Scanner(List<string> splitCode) {
            List<Token> output = new List<Token>();
            List<string> identifiers = new List<string>(new string[] { "int", "float", "string", "double", "bool", "char" });
            List<string> symbols = new List<string>(new string[] { "+", "-", "/", "%", "*", "(", ")", "{", "}", ",", ";", "&&",
                                                                        "||", "<", ">", "=", "!","++","==",">=","<=","!=" });
            List<string> reservedWords = new List<string>(new string[] { "for", "while", "if", "do", "return", "break", "continue", "end" });
            bool match = false;


            for (int i = 0; i < splitCode.Count; i++) {
                if (identifiers.Contains(splitCode[i]) && match == false) {
                    output.Add(new Token(splitCode[i], "Identificador"));
                    match = true;
                }
                if (symbols.Contains(splitCode[i]) && match == false) {
                    output.Add(new Token(splitCode[i], "Símbolo"));
                    match = true;
                }
                if (reservedWords.Contains(splitCode[i]) && match == false) {
                    output.Add(new Token(splitCode[i], "Palabra reservada"));
                    match = true;
                }
                if (float.TryParse(splitCode[i], out _) && match == false) {
                    output.Add(new Token(splitCode[i], "Número"));
                    match = true;
                }
                if (isValidVar(splitCode[i]) && match == false) {

                    variable pnn = new variable();
                    pnn.name = splitCode[i];
                    Lvar.Add(pnn);
                    output.Add(new Token(splitCode[i], "Variable"));
                    match = true;
                }
                if (splitCode[i].StartsWith("'") && splitCode[i].EndsWith("'") && match == false) {
                    output.Add(new Token(splitCode[i], "String"));
                    match = true;
                }
                if (match == false) {
                    output.Add(new Token(splitCode[i], "Desconocido"));
                }
                match = false;
            }
            return output;

            bool isValidVar(string v) {
                if (v.Length >= 1) {
                    if (char.IsLetter(v[0]) || v[0] == '_') {
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    return false;
                }
            }
        }


        List<string> SemanticAnalyzer(List<Token> tokens) {
            List<string> errors = new List<string>();
            Token prevInput1 = new Token();
            Token prevInput2 = new Token();
            Token prevInput3 = new Token();

            int selectedRule = 0;
            for (int i = 0; i < tokens.Count; i++) {
                if (selectedRule == 0) {
                    if (Rule1(tokens[i]).StartsWith("Start")) {
                        selectedRule = 1;
                        continue;
                    }
                    if (Rule2(tokens[i]).StartsWith("Start")) {
                        selectedRule = 2;
                        continue;
                    }
                    if (Rule3(tokens[i]).StartsWith("Start")) {
                        selectedRule = 3;
                        continue;
                    }
                }

                if (selectedRule == 1) {
                    var state = Rule1(tokens[i]);
                    if (state.StartsWith("Ok") || state.StartsWith("Error")) {
                        errors.Add(state);
                        selectedRule = 0;
                    }
                }
                if (selectedRule == 2) {
                    var state = Rule2(tokens[i]);
                    if (state.StartsWith("Ok") || state.StartsWith("Error")) {
                        errors.Add(state);
                        selectedRule = 0;
                    }
                }
                if (selectedRule == 3) {
                    var state = Rule3(tokens[i]);
                    if (state.StartsWith("Ok") || state.StartsWith("Error")) {
                        errors.Add(state);
                        selectedRule = 0;
                    }
                }
            }

            if (selectedRule == 1) {
                errors.Add(Rule1(new Token()));
            }
            if (selectedRule == 2) {
                errors.Add(Rule2(new Token()));
            }
            if (selectedRule == 3) {
                errors.Add(Rule3(new Token()));
            }

            string Rule1(Token input) {
                List<string> identifiers = new List<string>(new string[] { "int", "float", "string", "double", "bool", "char" });
                if (prevInput1.name == "" && input.type == "identifier") {
                    prevInput1 = input;
                    if (prevInput1.name == "int") {
                        f = 1;
                    }
                    if (prevInput1.name == "float") {
                        f = 2;
                    }
                    if (prevInput1.name == "string") {
                        f = 3;
                    }
                    if (prevInput1.name == "double") {
                        f = 4;
                    }
                    if (prevInput1.name == "bool") {
                        f = 5;
                    }
                    if (prevInput1.name == "char") {
                        f = 6;
                    }
                    return "Start Rule 1";
                } else if (prevInput1.type == "identifier") {
                    string state = Rule2(input);
                    if (state.StartsWith("Ok")) {
                        prevInput1 = new Token();
                    }
                    if (state != "Error Rule 2") {
                        return state.Substring(0, state.IndexOf("Rule 2") - 1) + " Rule 1";
                    }
                }
                if (prevInput1.type == "identifier") {
                    prevInput1 = new Token();
                    return "Error Expected 'variable' Rule 1";
                }
                prevInput1 = new Token();
                return "Error Rule 1";
            }

            string Rule2(Token input) {
                List<string> operators = new List<string>(new string[] { "+", "-", "/", "%", "*" });

                if (prevInput2.name == "" && input.type == "variable") {
                    prevInput2 = input;

                    return "Start Rule 2";
                } else if (prevInput2.type == "variable" && input.name == ";") {
                    prevInput2 = new Token();
                    fstop = 1;
                    return "Ok Rule 2";
                } else if (prevInput2.type == "variable" && input.name == "=") {
                    firstvar = prevInput2.name;
                    prevvar = prevInput2.name;
                    prevInput2 = input;

                    return "Continue Rule 2";
                } else if (prevInput2.name == "=" && input.type == "variable") {
                    prevInput2 = input;
                    prevtypevar = input.name;
                    if (f == 1) {
                        flagoutput = 0;
                        for (int i = 0; i < Lvar.Count; i++) {
                            if (Lvar[i].name == prevInput2.name && flagoutput == 0) {
                                for (int j = 0; j < Lvar.Count; j++) {
                                    if (Lvar[j].name == prevvar) {
                                        if (fstop == 1) {
                                            Lvar[j].integer = Lvar[i].integer;
                                            varsize.Add(Convert.ToString(Lvar[j].integer));

                                            flagoutput = 1;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return "Continue Rule 2";
                } else if (prevInput2.name == "=" && input.type == "number") {
                    prevInput2 = input;
                    if (f == 1) {
                        varsize.Add(prevInput2.name);
                        Lvar[ct].integer = Convert.ToInt32(prevInput2.name);
                    }

                    return "Continue Rule 2";
                } else if (prevInput2.type == "number" && input.name == ";") {
                    prevInput2 = new Token();
                    ct++;
                    return "Ok Rule 2";
                } else if (prevInput2.type == "number" && operators.Contains(input.name)) {
                    if (f == 1) {
                        number = Convert.ToInt32(prevInput2.name);
                        number2 = Convert.ToInt32(prevInput2.name);
                        prevtype = prevInput2.type;
                    }
                    prevInput2 = input;

                    return "Continue Rule 2";
                } else if (prevInput2.type == "variable" && operators.Contains(input.name)) {
                    prevvar = prevInput2.name;
                    vartype = prevInput2.type;
                    fstop = 1;
                    prevInput2 = input;
                    return "Continue Rule 2";
                } else if (operators.Contains(prevInput2.name) && input.type == "number") {
                    if (prevInput2.name == "+") {
                        if (f == 1 && prevtype == "number") {
                            Lvar[ct].integer = number + Convert.ToInt32(input.name);
                            varsize.Add(Convert.ToString(Lvar[ct].integer));
                            number2 = Convert.ToInt32(input.name);
                        }
                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == prevtypevar && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            Lvar[j].integer = Lvar[i].integer + Convert.ToInt32(input.name);
                                            varsize.Add(Convert.ToString(Lvar[j].integer));

                                            flagoutput = 1;
                                            break;
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prevInput2.name == "-") {
                        if (f == 1 && prevtype == "number") {
                            Lvar[ct].integer = number - Convert.ToInt32(input.name);
                            varsize.Add(Convert.ToString(Lvar[ct].integer));
                        }

                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == prevtypevar && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            Lvar[j].integer = Lvar[i].integer - Convert.ToInt32(input.name);
                                            varsize.Add(Convert.ToString(Lvar[j].integer));

                                            flagoutput = 1;
                                            break;
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prevInput2.name == "*") {
                        if (f == 1 && prevtype == "number") {
                            Lvar[ct].integer = number * Convert.ToInt32(input.name);
                            varsize.Add(Convert.ToString(Lvar[ct].integer));
                            number2 = Convert.ToInt32(input.name);
                        }
                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == prevtypevar && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            Lvar[j].integer = Lvar[i].integer * Convert.ToInt32(input.name);
                                            varsize.Add(Convert.ToString(Lvar[j].integer));

                                            flagoutput = 1;
                                            break;
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prevInput2.name == "/") {
                        if (f == 1 && prevtype == "number") {
                            Lvar[ct].integer = number / Convert.ToInt32(input.name);
                            varsize.Add(Convert.ToString(Lvar[ct].integer));
                        }
                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == prevtypevar && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            Lvar[j].integer = Lvar[i].integer / Convert.ToInt32(input.name);
                                            varsize.Add(Convert.ToString(Lvar[j].integer));

                                            flagoutput = 1;
                                            break;
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prevInput2.name == "%") {
                        if (f == 1 && prevtype == "number") {
                            Lvar[ct].integer = number % Convert.ToInt32(input.name);
                            varsize.Add(Convert.ToString(Lvar[ct].integer));
                        }
                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == prevtypevar && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            Lvar[j].integer = Lvar[i].integer % Convert.ToInt32(input.name);
                                            varsize.Add(Convert.ToString(Lvar[j].integer));

                                            flagoutput = 1;
                                            break;
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    prevInput2 = input;
                    return "Continue Rule 2";
                } else if (operators.Contains(prevInput2.name) && input.type == "variable") {
                    if (prevInput2.name == "+") {
                        fstop = 0;

                        if (f == 1 && prevtype == "number") {
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            if (fstop == 0) {
                                                Lvar[j].integer = number2 + Lvar[i].integer;

                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }

                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == prevvar) {
                                            for (int k = 0; k < Lvar.Count; k++) {
                                                if (firstvar == Lvar[k].name) {
                                                    Lvar[k].integer = Lvar[j].integer + Lvar[i].integer;
                                                    varsize.Add(Convert.ToString(Lvar[k].integer));

                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if (flagoutput == 1) {
                                            break;
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prevInput2.name == "-") {
                        fstop = 0;

                        if (f == 1 && prevtype == "number") {
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            if (fstop == 0) {
                                                Lvar[j].integer = number2 - Lvar[i].integer;

                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }

                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == prevvar) {
                                            for (int k = 0; k < Lvar.Count; k++) {
                                                if (firstvar == Lvar[k].name) {
                                                    Lvar[k].integer = Lvar[j].integer - Lvar[i].integer;
                                                    varsize.Add(Convert.ToString(Lvar[k].integer));

                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if (flagoutput == 1) {
                                            break;
                                        }

                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prevInput2.name == "*") {
                        fstop = 0;

                        if (f == 1 && prevtype == "number") {
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            if (fstop == 0) {
                                                Lvar[j].integer = number2 * Lvar[i].integer;

                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }

                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == prevvar) {
                                            for (int k = 0; k < Lvar.Count; k++) {
                                                if (firstvar == Lvar[k].name) {
                                                    Lvar[k].integer = Lvar[j].integer * Lvar[i].integer;
                                                    varsize.Add(Convert.ToString(Lvar[k].integer));

                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if (flagoutput == 1) {
                                            break;
                                        }
                                    }

                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (prevInput2.name == "/") {
                        fstop = 0;
                        if (f == 1 && prevtype == "number") {
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            if (fstop == 0) {
                                                Lvar[j].integer = number2 / Lvar[i].integer;

                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }

                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == prevvar) {
                                            for (int k = 0; k < Lvar.Count; k++) {
                                                if (firstvar == Lvar[k].name) {
                                                    Lvar[k].integer = Lvar[j].integer / Lvar[i].integer;
                                                    varsize.Add(Convert.ToString(Lvar[k].integer));

                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if (flagoutput == 1) {
                                            break;
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prevInput2.name == "%") {
                        fstop = 0;
                        if (f == 1 && prevtype == "number") {
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == firstvar) {
                                            if (fstop == 0) {
                                                Lvar[j].integer = number2 % Lvar[i].integer;

                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }

                        if (f == 1 && vartype == "variable") {
                            flagoutput = 0;
                            for (int i = 0; i < Lvar.Count; i++) {
                                if (Lvar[i].name == input.name && flagoutput == 0) {
                                    for (int j = 0; j < Lvar.Count; j++) {
                                        if (Lvar[j].name == prevvar) {
                                            for (int k = 0; k < Lvar.Count; k++) {
                                                if (firstvar == Lvar[k].name) {
                                                    Lvar[k].integer = Lvar[j].integer % Lvar[i].integer;
                                                    varsize.Add(Convert.ToString(Lvar[k].integer));

                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if (flagoutput == 1) {
                                            break;
                                        }
                                    }
                                    if (flagoutput == 1) {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    prevInput2 = input;
                    return "Continue Rule 2";
                }

                if (prevInput2.type == "variable") {
                    prevInput2 = new Token();
                    return "Error Expected ';' Or '='";
                }
                if (prevInput2.name == "=") {
                    prevInput2 = new Token();
                    return "Error Expected 'number' Or 'variable'";
                }
                if (prevInput2.type == "variable") {
                    prevInput2 = new Token();
                    return "Error Expected ';' Or 'operator'";
                }
                if (prevInput2.type == "number") {
                    prevInput2 = new Token();
                    return "Error Expected ';' Or 'operator'";
                }
                if (operators.Contains(prevInput2.name)) {
                    prevInput2 = new Token();
                    return "Error Expected 'number' Or 'variable'";
                }
                prevInput2 = new Token();
                return "Error Rule 2";

            }

            string Rule3(Token input) {
                List<string> comp_operators = new List<string>(new string[] { "==", "!=", "<=", "<", ">", ">=" });
                List<string> bool_operators = new List<string>(new string[] { "&&", "||" });
                if (prevInput3.name == "" && input.name == "if") {
                    prevInput3 = input;
                    return "Start Rule 3";
                } else if (prevInput3.name == "if" && input.name == "(") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (prevInput3.name == "(" && input.type == "variable") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (prevInput3.type == "variable" && comp_operators.Contains(input.name)) {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (comp_operators.Contains(prevInput3.name) && input.type == "number") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (comp_operators.Contains(prevInput3.name) && input.type == "variable") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (prevInput3.type == "number" && bool_operators.Contains(input.name)) {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (prevInput3.type == "variable" && bool_operators.Contains(input.name)) {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (bool_operators.Contains(prevInput3.name) && input.type == "variable") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (prevInput3.type == "number" && input.name == ")") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (prevInput3.type == "variable" && input.name == ")") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (prevInput3.name == ")" && input.name == "{") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if (prevInput3.name == "{" && input.name == "}") {
                    prevInput3 = new Token();
                    return "Ok Rule 3";
                } else if (prevInput3.name == "{" && input.name != "") {
                    return "Continue Rule 3";
                }

                if (prevInput3.name == "if") {
                    prevInput3 = new Token();
                    return "Error Expected '('";
                }
                if (prevInput3.name == "(") {
                    prevInput3 = new Token();
                    return "Error Expected 'variable'";
                }
                if (prevInput3.type == "variable") {
                    prevInput3 = new Token();
                    return "Error Expected 'comp_operator' Or 'bool_operator' Or ')'";
                }
                if (comp_operators.Contains(prevInput3.name)) {
                    prevInput3 = new Token();
                    return "Error Expected 'number' Or 'variable'";
                }
                if (bool_operators.Contains(prevInput3.name)) {
                    prevInput3 = new Token();
                    return "Error Expected 'variable'";
                }
                if (prevInput3.type == "number") {
                    prevInput3 = new Token();
                    return "Error Expected 'bool_operator' Or ')'";
                }
                if (prevInput3.name == ")") {
                    prevInput3 = new Token();
                    return "Error Expected '{'";
                }
                if (prevInput3.name == "{") {
                    prevInput3 = new Token();
                    return "Error Expected '}'";
                }

                prevInput3 = new Token();
                return "Error Rule 3";
            }
            return errors;
        }
        //-------------------------------------------  AnalizadorSemántico  FIN -------------------------------------------------------------



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
