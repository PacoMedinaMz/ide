/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ide;

import ide.Config.Opcion;
import static ide.IDELogger.log;
import java.awt.Font;
import java.awt.Toolkit;
import java.awt.datatransfer.DataFlavor;
import java.awt.event.KeyEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.OutputStreamWriter;
import java.util.Stack;
import javax.swing.AbstractAction;
import javax.swing.Action;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.KeyStroke;
import javax.swing.text.DefaultEditorKit;

/**
 *
 * @author Rodrigo Maafs
 */
public class Main extends javax.swing.JFrame {

    public static Config config = new Config();//Archivo de configuración
    private File archivoEditando = null;
    private String codeLength = "";//Longitud del código (Se usará para verificar si hay cambios en el archivo)
    private Stack<String> movimientos = new Stack<>();
    private Stack<String> movimientosY = new Stack<>();

    /**
     * Creates new form Main
     */
    public Main() {
        log("Iniciando IDE...");
        initComponents();
        initComponents2();
        initColors();
        
        //Vamos a abrir el código del último archivo abierto antes de cerrar el IDE
        if (!config.get(Opcion.LAST_FILE).equals("")) {//Si en la configuración, existe la ruta "last_file"
            this.archivoEditando = new File(config.get(Opcion.LAST_FILE));//Creamos la instancia del archivo con la ruta del último archivo abierto.
            if (!this.archivoEditando.exists()) {//Si el archivo no existe, entonces no lo abrimos.
                this.archivoEditando = null;
            }

            loadCodeFromFile(this.archivoEditando);
        }

        this.addWindowListener(new WindowAdapter() {
            @Override
            public void windowClosing(WindowEvent event) {
                if (hayCambios() && preguntar("¿Quieres guardar los cambios en el archivo?", "Cambios sin guardar")) {
                    guardarArchivo();
                }
            }
        });
    }
    
    /**
     * Función que edita los colores de ciertos componentes
     */
    private void initColors() {
        getContentPane().setBackground(new java.awt.Color(60, 63, 65));
    }
    
    /**
     * Función para seguir modificando los componentes gráficos.
     */
    private void initComponents2() {
        AbstractAction pasteAction = new DefaultEditorKit.PasteAction();
        pasteAction.putValue(Action.ACCELERATOR_KEY, KeyStroke.getKeyStroke(KeyEvent.VK_V, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
        menuPegar.setAction(pasteAction);
        menuPegar.setText("Pegar");

        AbstractAction copyAction = new DefaultEditorKit.CopyAction();
        copyAction.putValue(Action.ACCELERATOR_KEY, KeyStroke.getKeyStroke(KeyEvent.VK_C, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
        menuCopiar.setAction(copyAction);
        menuCopiar.setText("Copiar");
        
        AbstractAction cutAction = new DefaultEditorKit.CutAction();
        cutAction.putValue(Action.ACCELERATOR_KEY, KeyStroke.getKeyStroke(KeyEvent.VK_X, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
        menuCortar.setAction(cutAction);
        menuCortar.setText("Cortar");
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jPopupMenu1 = new javax.swing.JPopupMenu();
        jScrollPane1 = new javax.swing.JScrollPane();
        txtCodigo = new javax.swing.JTextArea();
        txtLabelCodigo = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        txtErrores = new javax.swing.JLabel();
        jTabbedPane2 = new javax.swing.JTabbedPane();
        jPanel2 = new javax.swing.JPanel();
        jPanel3 = new javax.swing.JPanel();
        jPanel4 = new javax.swing.JPanel();
        txtCambios = new javax.swing.JLabel();
        jMenuBar1 = new javax.swing.JMenuBar();
        jMenu1 = new javax.swing.JMenu();
        menuNuevoArchivo = new javax.swing.JMenuItem();
        menuAbrirArchivo = new javax.swing.JMenuItem();
        menuGuardar = new javax.swing.JMenuItem();
        menuGuardarComo = new javax.swing.JMenuItem();
        jMenu2 = new javax.swing.JMenu();
        menuUndo = new javax.swing.JMenuItem();
        menuRedo = new javax.swing.JMenuItem();
        menuCortar = new javax.swing.JMenuItem();
        menuCopiar = new javax.swing.JMenuItem();
        menuPegar = new javax.swing.JMenuItem();
        menuBorrar = new javax.swing.JMenuItem();
        menuBuscar = new javax.swing.JMenuItem();
        menuReemplazar = new javax.swing.JMenuItem();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setBackground(new java.awt.Color(102, 0, 204));

        txtCodigo.setBackground(new java.awt.Color(43, 43, 43));
        txtCodigo.setColumns(20);
        txtCodigo.setForeground(new java.awt.Color(255, 255, 255));
        txtCodigo.setRows(5);
        txtCodigo.setCaretColor(new java.awt.Color(255, 255, 255));
        txtCodigo.addKeyListener(new java.awt.event.KeyAdapter() {
            public void keyReleased(java.awt.event.KeyEvent evt) {
                txtCodigoKeyReleased(evt);
            }
            public void keyTyped(java.awt.event.KeyEvent evt) {
                txtCodigoKeyTyped(evt);
            }
        });
        jScrollPane1.setViewportView(txtCodigo);

        txtLabelCodigo.setBackground(new java.awt.Color(60, 63, 65));
        txtLabelCodigo.setForeground(new java.awt.Color(255, 255, 255));
        txtLabelCodigo.setText("Código:");
        txtLabelCodigo.setOpaque(true);

        jLabel2.setBackground(new java.awt.Color(60, 63, 65));
        jLabel2.setForeground(new java.awt.Color(255, 255, 255));
        jLabel2.setText("Errores");
        jLabel2.setOpaque(true);

        txtErrores.setBackground(new java.awt.Color(43, 43, 43));
        txtErrores.setForeground(new java.awt.Color(255, 255, 255));
        txtErrores.setText("No hay errores");
        txtErrores.setVerticalAlignment(javax.swing.SwingConstants.TOP);
        txtErrores.setOpaque(true);

        jTabbedPane2.setBackground(new java.awt.Color(60, 63, 65));
        jTabbedPane2.setForeground(new java.awt.Color(255, 255, 255));
        jTabbedPane2.setName("Léxico"); // NOI18N
        jTabbedPane2.setOpaque(true);

        jPanel2.setBackground(new java.awt.Color(43, 43, 43));

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 509, Short.MAX_VALUE)
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 311, Short.MAX_VALUE)
        );

        jTabbedPane2.addTab("Léxico", jPanel2);

        jPanel3.setBackground(new java.awt.Color(43, 43, 43));
        jPanel3.setForeground(new java.awt.Color(255, 255, 255));

        javax.swing.GroupLayout jPanel3Layout = new javax.swing.GroupLayout(jPanel3);
        jPanel3.setLayout(jPanel3Layout);
        jPanel3Layout.setHorizontalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 509, Short.MAX_VALUE)
        );
        jPanel3Layout.setVerticalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 311, Short.MAX_VALUE)
        );

        jTabbedPane2.addTab("Sintáctico", jPanel3);

        jPanel4.setBackground(new java.awt.Color(43, 43, 43));
        jPanel4.setForeground(new java.awt.Color(255, 255, 255));

        javax.swing.GroupLayout jPanel4Layout = new javax.swing.GroupLayout(jPanel4);
        jPanel4.setLayout(jPanel4Layout);
        jPanel4Layout.setHorizontalGroup(
            jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 509, Short.MAX_VALUE)
        );
        jPanel4Layout.setVerticalGroup(
            jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 311, Short.MAX_VALUE)
        );

        jTabbedPane2.addTab("Semántico", jPanel4);

        txtCambios.setBackground(new java.awt.Color(60, 63, 65));
        txtCambios.setForeground(new java.awt.Color(255, 255, 255));
        txtCambios.setOpaque(true);

        jMenuBar1.setBackground(new java.awt.Color(60, 63, 65));
        jMenuBar1.setForeground(new java.awt.Color(255, 255, 255));

        jMenu1.setBackground(new java.awt.Color(204, 204, 204));
        jMenu1.setForeground(new java.awt.Color(204, 204, 204));
        jMenu1.setText("Archivo");

        menuNuevoArchivo.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_N, java.awt.event.InputEvent.CTRL_MASK));
        menuNuevoArchivo.setText("Nuevo archivo");
        menuNuevoArchivo.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuNuevoArchivoActionPerformed(evt);
            }
        });
        jMenu1.add(menuNuevoArchivo);

        menuAbrirArchivo.setText("Abrir archivo");
        menuAbrirArchivo.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuAbrirArchivoActionPerformed(evt);
            }
        });
        jMenu1.add(menuAbrirArchivo);

        menuGuardar.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_S, java.awt.event.InputEvent.CTRL_MASK));
        menuGuardar.setText("Guardar");
        menuGuardar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuGuardarActionPerformed(evt);
            }
        });
        jMenu1.add(menuGuardar);

        menuGuardarComo.setText("Guardar como");
        menuGuardarComo.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuGuardarComoActionPerformed(evt);
            }
        });
        jMenu1.add(menuGuardarComo);

        jMenuBar1.add(jMenu1);

        jMenu2.setForeground(new java.awt.Color(204, 204, 204));
        jMenu2.setText("Edit");

        menuUndo.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_Z, java.awt.event.InputEvent.CTRL_MASK));
        menuUndo.setText("Undo");
        menuUndo.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuUndoActionPerformed(evt);
            }
        });
        jMenu2.add(menuUndo);

        menuRedo.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_Y, java.awt.event.InputEvent.CTRL_MASK));
        menuRedo.setText("Redo");
        menuRedo.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuRedoActionPerformed(evt);
            }
        });
        jMenu2.add(menuRedo);

        menuCortar.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_X, java.awt.event.InputEvent.CTRL_MASK));
        menuCortar.setText("Cortar");
        menuCortar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuCortarActionPerformed(evt);
            }
        });
        jMenu2.add(menuCortar);

        menuCopiar.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_C, java.awt.event.InputEvent.CTRL_MASK));
        menuCopiar.setText("Copiar");
        menuCopiar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuCopiarActionPerformed(evt);
            }
        });
        jMenu2.add(menuCopiar);

        menuPegar.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_V, java.awt.event.InputEvent.CTRL_MASK));
        menuPegar.setText("Pegar");
        menuPegar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuPegarActionPerformed(evt);
            }
        });
        jMenu2.add(menuPegar);

        menuBorrar.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_DELETE, 0));
        menuBorrar.setText("Borrar");
        menuBorrar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuBorrarActionPerformed(evt);
            }
        });
        jMenu2.add(menuBorrar);

        menuBuscar.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_F, java.awt.event.InputEvent.CTRL_MASK));
        menuBuscar.setText("Buscar");
        jMenu2.add(menuBuscar);

        menuReemplazar.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_H, java.awt.event.InputEvent.SHIFT_MASK | java.awt.event.InputEvent.CTRL_MASK));
        menuReemplazar.setText("Reemplazar");
        jMenu2.add(menuReemplazar);

        jMenuBar1.add(jMenu2);

        setJMenuBar(jMenuBar1);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(txtErrores, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 529, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(jTabbedPane2))
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel2)
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(txtLabelCodigo)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(txtCambios)))
                        .addGap(0, 0, Short.MAX_VALUE)))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(34, 34, 34)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(txtLabelCodigo)
                    .addComponent(txtCambios))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jTabbedPane2)
                    .addComponent(jScrollPane1))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 39, Short.MAX_VALUE)
                .addComponent(jLabel2)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(txtErrores, javax.swing.GroupLayout.PREFERRED_SIZE, 116, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );

        jTabbedPane2.getAccessibleContext().setAccessibleName("Léxico");

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void menuNuevoArchivoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuNuevoArchivoActionPerformed
        //Al clickear en "Nuevo archivo"...
        File preArchivo = this.archivoEditando;//Guardamos la instancia del archivo en caso de que tenga uno abierto
        this.archivoEditando = null;//Decimos que el archivo aún no está guardado.
        guardarArchivo();

        //Si no guardó el archivo, pero anteriormente tenía una instancia del archivo creado...
        if (preArchivo != null && this.archivoEditando == null) {
            this.archivoEditando = preArchivo;//Regresamos la instancia a como estaba
        } else {
            txtCodigo.setText("");//Eliminamos el texto que tenga el código
            txtCodigo.requestFocusInWindow();//Añadimos el puntero sobre el código para estar listo para escribir.
        }
        this.setTitle(this.archivoEditando == null ? "" : this.archivoEditando.getName());
    }//GEN-LAST:event_menuNuevoArchivoActionPerformed

    private void menuGuardarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuGuardarActionPerformed
        guardarArchivo();
    }//GEN-LAST:event_menuGuardarActionPerformed

    private void menuAbrirArchivoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuAbrirArchivoActionPerformed
        File file = dialogLocation("¿Cuál archivo desea abrir?");
        if (file != null) {//Si seleccionó una locación...
            this.archivoEditando = file;//Guardamos la instancia del archivo.
        } else {
            msg("Se canceló el leer el archivo");
            return;
        }

        loadCodeFromFile(file);//Leemos el código del archivo
    }//GEN-LAST:event_menuAbrirArchivoActionPerformed

    private void menuGuardarComoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuGuardarComoActionPerformed
        File preArchivo = this.archivoEditando;//Guardamos la instancia del archivo en caso de que tenga uno abierto
        this.archivoEditando = null;//Eliminamos la instancia del archivo para forzar que pida locación para guardar el archivo.
        guardarArchivo();//Solicitamos guardar el archivo

        //Si no guardó el archivo, pero anteriormente tenía una instancia del archivo creado...
        if (preArchivo != null && this.archivoEditando == null) {
            this.archivoEditando = preArchivo;//Regresamos la instancia a como estaba
        }
        this.setTitle(this.archivoEditando == null ? "" : this.archivoEditando.getName());
    }//GEN-LAST:event_menuGuardarComoActionPerformed

    private void txtCodigoKeyReleased(java.awt.event.KeyEvent evt) {//GEN-FIRST:event_txtCodigoKeyReleased
        refreshLenght();
    }//GEN-LAST:event_txtCodigoKeyReleased

    private void menuUndoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuUndoActionPerformed
        removeMovimiento();
    }//GEN-LAST:event_menuUndoActionPerformed

    private void txtCodigoKeyTyped(java.awt.event.KeyEvent evt) {//GEN-FIRST:event_txtCodigoKeyTyped
//        System.out.println(evt.paramString());
        if (evt.paramString().contains("")) {//CTRL + Z, Mágicamente, si elimino esta línea, no sirve el undo CTRL + Z, #VivaJava
        } else if (evt.paramString().contains("")) {//CTRL + Y, Igual mágicamente si quito este if, no sirve. #VivaJava
        } else if (evt.paramString().contains("")) {//CTRL + V
            clickPegar();
        } else if (evt.paramString().contains("keyChar=Cancelar,modifiers=Ctrl")) {//CTRL + X
            clickCortar();
        } else {
            addMovimiento();
        }
    }//GEN-LAST:event_txtCodigoKeyTyped

    private void menuPegarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuPegarActionPerformed
        log("Pegar");
        clickPegar();
    }//GEN-LAST:event_menuPegarActionPerformed

    private void menuRedoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuRedoActionPerformed
        log("Redo");
        if (!movimientosY.isEmpty()) {
            addMovimiento();
            String nuevo = movimientosY.pop();
            txtCodigo.setText(nuevo);
        }
    }//GEN-LAST:event_menuRedoActionPerformed

    private void menuCopiarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuCopiarActionPerformed
        log("Copiar");
        clickCopiar();
    }//GEN-LAST:event_menuCopiarActionPerformed

    private void menuCortarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuCortarActionPerformed
        log("Cortar");
        clickCortar();
    }//GEN-LAST:event_menuCortarActionPerformed

    private void menuBorrarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuBorrarActionPerformed
        log("Borrar");
        clickBorrar();
    }//GEN-LAST:event_menuBorrarActionPerformed

    private void clickPegar() {
        addMovimiento();
        refreshLenght();
    }

    private void clickCopiar() {
        refreshLenght();
    }
    
    private void clickCortar() {
        refreshLenght();
    }
    
    private void clickBorrar() {
        txtCodigo.replaceSelection("");
        refreshLenght();
    }

    private void addMovimiento() {
        movimientos.push(txtCodigo.getText());
    }

    private void removeMovimiento() {
        movimientosY.push(txtCodigo.getText());
        String aremover = movimientos.size() > 1 ? movimientos.pop() : movimientos.peek();
        txtCodigo.setText(aremover);
    }

    private String getTextClipBoard() {
        String clipboard = "";
        try {
            clipboard = (String) Toolkit.getDefaultToolkit().getSystemClipboard().getData(DataFlavor.stringFlavor);
        } catch (Exception e) {
        }
        return clipboard;
    }

    private void refreshLenght() {
        //Función que se ejecuta cuando escribe un nuevo carácter en el código.
        boolean hayCambios = hayCambios();
        txtCambios.setFont(new Font(Font.DIALOG, hayCambios ? Font.BOLD : Font.PLAIN, 12));
        txtCambios.setText(hayCambios ? " (Cambios sin guardar)" : "");
    }

    /**
     * Función que nos dice si hay cambios en el archivo editando.
     *
     * @return Retornará verdadero si hay cambios en el código a editar.
     */
    private boolean hayCambios() {
        return !txtCodigo.getText().equals(codeLength);
    }

    /**
     * Función para la funcionalidad para guardar archivo.
     */
    private void guardarArchivo() {
        //Si el archivo, NO había sido previamente creado
        if (archivoEditando == null) {
            //Le preguntamos donde lo quiere guardar
            File file = dialogLocation("¿Donde desea guardar el archivo?");
            if (file != null) {//Si seleccionó una locación...
                this.archivoEditando = file;//Guardamos la instancia del archivo.
            } else {
                msg("Se canceló el guardar el archivo");
                return;
            }
        }

        //Escribimos todo nuestro código en el archivo.
        try {
            BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(this.archivoEditando)));
            bw.write(txtCodigo.getText());
            bw.close();
            log("Archivo guardado.");
        } catch (Exception e) {
            msg("Error al guardar el archivo: " + e.getMessage());
        }
        config.set(Opcion.LAST_FILE, this.archivoEditando.getAbsolutePath());//Guardamos en la configuración, la ruta del último archivo editado.
        this.setTitle(this.archivoEditando.getName());//Cambiamos el nombre de la ventana y le ponemos el nombre del archivo.
        this.codeLength = txtCodigo.getText();
        refreshLenght();
    }

    /**
     * Función que se encarga de abrir la ventana popup para seleccionar un
     * archivo o locación
     *
     * @param title Título de la ventana
     * @return
     */
    private File dialogLocation(String title) {
        JFrame parentFrame = new JFrame();
        JFileChooser fileChooser = new JFileChooser();
        fileChooser.setDialogTitle(title);
        int userSelection = fileChooser.showSaveDialog(parentFrame);

        //Si dió click al botón de "Aceptar"
        if (userSelection == JFileChooser.APPROVE_OPTION) {
            File fileToSave = fileChooser.getSelectedFile();
            log("Arhivo seleccionado: " + fileToSave.getAbsolutePath());
            return fileToSave;
        }
        return null;
    }

    private void loadCodeFromFile(File file) {
        StringBuilder codigo = new StringBuilder("");
        try {
            BufferedReader reader = new BufferedReader(new FileReader(file));
            String line = "";
            while (line != null) {//Ciclamos todas las líneas del archivo
                line = reader.readLine();
                if (line == null) {
                    continue;
                }

                codigo.append(line).append("\n");
            }
            reader.close();
        } catch (Exception e) {
            msg("Error al leer el archivo: " + e.getMessage());
        }

        txtCodigo.setText(codigo.toString());
        codeLength = codigo.toString();
        this.setTitle(file.getName());//Cambiamos el nombre de la ventana y le ponemos el nombre del archivo.
        config.set(Opcion.LAST_FILE, this.archivoEditando.getAbsolutePath());//Guardamos en la configuración, la ruta del último archivo editado.

        //Limpiamos los movimientos
        movimientos.clear();
        addMovimiento();
    }

    /**
     * Función que nos abre una ventana popup con un mensaje de alerta.
     *
     * @param msg Alerta a mostrar
     */
    private void msg(String msg) {
        JOptionPane.showMessageDialog(null, msg);
    }

    /**
     * Función que nos abre una ventana popup con una pregunta.
     *
     * @param msg Pregunta
     * @param title Título de la ventana
     * @return Retornará verdadero si el usuario dió click en YES
     */
    private boolean preguntar(String msg, String title) {
        return JOptionPane.showConfirmDialog(null, msg, title, JOptionPane.YES_NO_OPTION) == JOptionPane.YES_OPTION;
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(Main.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(Main.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(Main.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(Main.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new Main().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JLabel jLabel2;
    private javax.swing.JMenu jMenu1;
    private javax.swing.JMenu jMenu2;
    private javax.swing.JMenuBar jMenuBar1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JPanel jPanel3;
    private javax.swing.JPanel jPanel4;
    private javax.swing.JPopupMenu jPopupMenu1;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JTabbedPane jTabbedPane2;
    private javax.swing.JMenuItem menuAbrirArchivo;
    private javax.swing.JMenuItem menuBorrar;
    private javax.swing.JMenuItem menuBuscar;
    private javax.swing.JMenuItem menuCopiar;
    private javax.swing.JMenuItem menuCortar;
    private javax.swing.JMenuItem menuGuardar;
    private javax.swing.JMenuItem menuGuardarComo;
    private javax.swing.JMenuItem menuNuevoArchivo;
    private javax.swing.JMenuItem menuPegar;
    private javax.swing.JMenuItem menuRedo;
    private javax.swing.JMenuItem menuReemplazar;
    private javax.swing.JMenuItem menuUndo;
    private javax.swing.JLabel txtCambios;
    private javax.swing.JTextArea txtCodigo;
    private javax.swing.JLabel txtErrores;
    private javax.swing.JLabel txtLabelCodigo;
    // End of variables declaration//GEN-END:variables
}
