
namespace ProyCompilador
{
    partial class ventana
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ventana));
            this.label1 = new System.Windows.Forms.Label();
            this.panelarea = new System.Windows.Forms.Panel();
            this.pestanas = new System.Windows.Forms.TabControl();
            this.lexico = new System.Windows.Forms.TabPage();
            this.pLexico = new System.Windows.Forms.DataGridView();
            this.cNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLexema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cColumna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIndice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sintactico = new System.Windows.Forms.TabPage();
            this.pSintactico = new System.Windows.Forms.RichTextBox();
            this.semantico = new System.Windows.Forms.TabPage();
            this.pSemantico = new System.Windows.Forms.RichTextBox();
            this.panelResult = new System.Windows.Forms.Panel();
            this.log = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.comp = new System.Windows.Forms.TabControl();
            this.tabCodigo = new System.Windows.Forms.TabPage();
            this.txtCode = new System.Windows.Forms.RichTextBox();
            this.pCodigo = new System.Windows.Forms.Panel();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.pesArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.opAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.opSave = new System.Windows.Forms.ToolStripMenuItem();
            this.opSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.opExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pesEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.opCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.opPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.opCut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.opUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.opRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.opSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCompilar = new System.Windows.Forms.Button();
            this.panelarea.SuspendLayout();
            this.pestanas.SuspendLayout();
            this.lexico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLexico)).BeginInit();
            this.sintactico.SuspendLayout();
            this.semantico.SuspendLayout();
            this.panelResult.SuspendLayout();
            this.log.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.comp.SuspendLayout();
            this.tabCodigo.SuspendLayout();
            this.pCodigo.SuspendLayout();
            this.tools.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panelarea
            // 
            resources.ApplyResources(this.panelarea, "panelarea");
            this.panelarea.Controls.Add(this.pestanas);
            this.panelarea.Name = "panelarea";
            // 
            // pestanas
            // 
            resources.ApplyResources(this.pestanas, "pestanas");
            this.pestanas.Controls.Add(this.lexico);
            this.pestanas.Controls.Add(this.sintactico);
            this.pestanas.Controls.Add(this.semantico);
            this.pestanas.Name = "pestanas";
            this.pestanas.SelectedIndex = 0;
            // 
            // lexico
            // 
            resources.ApplyResources(this.lexico, "lexico");
            this.lexico.Controls.Add(this.pLexico);
            this.lexico.Name = "lexico";
            this.lexico.UseVisualStyleBackColor = true;
            // 
            // pLexico
            // 
            resources.ApplyResources(this.pLexico, "pLexico");
            this.pLexico.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.pLexico.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pLexico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pLexico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNombre,
            this.cLexema,
            this.cColumna,
            this.cLinea,
            this.cIndice});
            this.pLexico.Name = "pLexico";
            this.pLexico.RowHeadersVisible = false;
            this.pLexico.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.pLexico.RowTemplate.Height = 25;
            this.pLexico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // cNombre
            // 
            resources.ApplyResources(this.cNombre, "cNombre");
            this.cNombre.Name = "cNombre";
            this.cNombre.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // cLexema
            // 
            resources.ApplyResources(this.cLexema, "cLexema");
            this.cLexema.Name = "cLexema";
            // 
            // cColumna
            // 
            resources.ApplyResources(this.cColumna, "cColumna");
            this.cColumna.Name = "cColumna";
            // 
            // cLinea
            // 
            resources.ApplyResources(this.cLinea, "cLinea");
            this.cLinea.Name = "cLinea";
            this.cLinea.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cLinea.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cIndice
            // 
            resources.ApplyResources(this.cIndice, "cIndice");
            this.cIndice.Name = "cIndice";
            // 
            // sintactico
            // 
            resources.ApplyResources(this.sintactico, "sintactico");
            this.sintactico.Controls.Add(this.pSintactico);
            this.sintactico.Name = "sintactico";
            this.sintactico.UseVisualStyleBackColor = true;
            // 
            // pSintactico
            // 
            resources.ApplyResources(this.pSintactico, "pSintactico");
            this.pSintactico.Name = "pSintactico";
            // 
            // semantico
            // 
            resources.ApplyResources(this.semantico, "semantico");
            this.semantico.Controls.Add(this.pSemantico);
            this.semantico.Name = "semantico";
            this.semantico.UseVisualStyleBackColor = true;
            // 
            // pSemantico
            // 
            resources.ApplyResources(this.pSemantico, "pSemantico");
            this.pSemantico.Name = "pSemantico";
            // 
            // panelResult
            // 
            resources.ApplyResources(this.panelResult, "panelResult");
            this.panelResult.Controls.Add(this.log);
            this.panelResult.Name = "panelResult";
            // 
            // log
            // 
            resources.ApplyResources(this.log, "log");
            this.log.Controls.Add(this.tabLog);
            this.log.Name = "log";
            this.log.SelectedIndex = 0;
            // 
            // tabLog
            // 
            resources.ApplyResources(this.tabLog, "tabLog");
            this.tabLog.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tabLog.Name = "tabLog";
            // 
            // txtLog
            // 
            resources.ApplyResources(this.txtLog, "txtLog");
            this.txtLog.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.txtLog.Name = "txtLog";
            // 
            // comp
            // 
            resources.ApplyResources(this.comp, "comp");
            this.comp.Controls.Add(this.tabCodigo);
            this.comp.Name = "comp";
            this.comp.SelectedIndex = 0;
            // 
            // tabCodigo
            // 
            resources.ApplyResources(this.tabCodigo, "tabCodigo");
            this.tabCodigo.BackColor = System.Drawing.SystemColors.GrayText;
            this.tabCodigo.Controls.Add(this.txtCode);
            this.tabCodigo.Name = "tabCodigo";
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // pCodigo
            // 
            resources.ApplyResources(this.pCodigo, "pCodigo");
            this.pCodigo.Controls.Add(this.comp);
            this.pCodigo.Name = "pCodigo";
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // btnCut
            // 
            resources.ApplyResources(this.btnCut, "btnCut");
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Name = "btnCut";
            this.btnCut.Click += new System.EventHandler(this.cutToolStripButton_Click);
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Click += new System.EventHandler(this.copyToolStripButton_Click);
            // 
            // btnPaste
            // 
            resources.ApplyResources(this.btnPaste, "btnPaste");
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Click += new System.EventHandler(this.pasteToolStripButton_Click);
            // 
            // tools
            // 
            resources.ApplyResources(this.tools, "tools");
            this.tools.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnSave,
            this.btnCopy,
            this.btnPaste,
            this.btnCut,
            this.btnUndo,
            this.btnRedo});
            this.tools.Name = "tools";
            this.tools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // btnUndo
            // 
            resources.ApplyResources(this.btnUndo, "btnUndo");
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Click += new System.EventHandler(this.undo_Click);
            // 
            // btnRedo
            // 
            resources.ApplyResources(this.btnRedo, "btnRedo");
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Click += new System.EventHandler(this.redo_Click);
            // 
            // menu
            // 
            resources.ApplyResources(this.menu, "menu");
            this.menu.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pesArchivo,
            this.pesEditar});
            this.menu.Name = "menu";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // pesArchivo
            // 
            resources.ApplyResources(this.pesArchivo, "pesArchivo");
            this.pesArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opAbrir,
            this.toolStripSeparator3,
            this.opSave,
            this.opSaveAs,
            this.opExit});
            this.pesArchivo.Name = "pesArchivo";
            // 
            // opAbrir
            // 
            resources.ApplyResources(this.opAbrir, "opAbrir");
            this.opAbrir.Name = "opAbrir";
            this.opAbrir.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // opSave
            // 
            resources.ApplyResources(this.opSave, "opSave");
            this.opSave.Name = "opSave";
            this.opSave.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // opSaveAs
            // 
            resources.ApplyResources(this.opSaveAs, "opSaveAs");
            this.opSaveAs.Name = "opSaveAs";
            this.opSaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // opExit
            // 
            resources.ApplyResources(this.opExit, "opExit");
            this.opExit.Name = "opExit";
            this.opExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pesEditar
            // 
            resources.ApplyResources(this.pesEditar, "pesEditar");
            this.pesEditar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opCopy,
            this.opPaste,
            this.opCut,
            this.toolStripSeparator5,
            this.opUndo,
            this.opRedo,
            this.opSelectAll});
            this.pesEditar.Name = "pesEditar";
            // 
            // opCopy
            // 
            resources.ApplyResources(this.opCopy, "opCopy");
            this.opCopy.Name = "opCopy";
            this.opCopy.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // opPaste
            // 
            resources.ApplyResources(this.opPaste, "opPaste");
            this.opPaste.Name = "opPaste";
            this.opPaste.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // opCut
            // 
            resources.ApplyResources(this.opCut, "opCut");
            this.opCut.Name = "opCut";
            this.opCut.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // opUndo
            // 
            resources.ApplyResources(this.opUndo, "opUndo");
            this.opUndo.Name = "opUndo";
            this.opUndo.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // opRedo
            // 
            resources.ApplyResources(this.opRedo, "opRedo");
            this.opRedo.Name = "opRedo";
            this.opRedo.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // opSelectAll
            // 
            resources.ApplyResources(this.opSelectAll, "opSelectAll");
            this.opSelectAll.Name = "opSelectAll";
            this.opSelectAll.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // btnCompilar
            // 
            resources.ApplyResources(this.btnCompilar, "btnCompilar");
            this.btnCompilar.Name = "btnCompilar";
            this.btnCompilar.UseVisualStyleBackColor = true;
            this.btnCompilar.Click += new System.EventHandler(this.btnCompilar_Click_1);
            // 
            // ventana
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.btnCompilar);
            this.Controls.Add(this.tools);
            this.Controls.Add(this.panelResult);
            this.Controls.Add(this.panelarea);
            this.Controls.Add(this.pCodigo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "ventana";
            this.panelarea.ResumeLayout(false);
            this.pestanas.ResumeLayout(false);
            this.lexico.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pLexico)).EndInit();
            this.sintactico.ResumeLayout(false);
            this.semantico.ResumeLayout(false);
            this.panelResult.ResumeLayout(false);
            this.log.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.comp.ResumeLayout(false);
            this.tabCodigo.ResumeLayout(false);
            this.pCodigo.ResumeLayout(false);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelarea;
        private System.Windows.Forms.TabControl pestanas;
        private System.Windows.Forms.TabPage lexico;
        private System.Windows.Forms.TabPage sintactico;
        private System.Windows.Forms.TabPage semantico;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.TabControl log;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabControl comp;
        private System.Windows.Forms.TabPage tabCodigo;
        private System.Windows.Forms.Panel pCodigo;
        private System.Windows.Forms.RichTextBox txtCode;
        private System.Windows.Forms.RichTextBox pSintactico;
        private System.Windows.Forms.RichTextBox pSemantico;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton redo;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem ayuda;
        private System.Windows.Forms.ToolStripMenuItem option;
        private System.Windows.Forms.ToolStripMenuItem opAbrir;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton undo;
        private System.Windows.Forms.DataGridView pLexico;
        private System.Windows.Forms.RichTextBox Sin;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripMenuItem pesArchivo;
        private System.Windows.Forms.ToolStripMenuItem pesEditar;
        private System.Windows.Forms.ToolStripMenuItem opSave;
        private System.Windows.Forms.ToolStripMenuItem opSaveAs;
        private System.Windows.Forms.ToolStripMenuItem opExit;
        private System.Windows.Forms.ToolStripMenuItem opCopy;
        private System.Windows.Forms.ToolStripMenuItem opPaste;
        private System.Windows.Forms.ToolStripMenuItem opCut;
        private System.Windows.Forms.ToolStripMenuItem opUndo;
        private System.Windows.Forms.ToolStripMenuItem opRedo;
        private System.Windows.Forms.ToolStripMenuItem opSelectAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLexema;
        private System.Windows.Forms.DataGridViewTextBoxColumn cColumna;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLinea;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIndice;
        private System.Windows.Forms.Button btnCompilar;
    }
}

