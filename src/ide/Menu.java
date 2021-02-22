package ide;

import java.awt.Color;
import java.awt.Font;
import static java.lang.System.out;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextArea;

public class Menu {
    private JFrame frame;
    private JPanel panel;
    private JTextArea codigo;
    private JTextArea consola;
    private JLabel cod;
    private JLabel con;
    
   Menu(){
        out.println("Hola");
        frame = new JFrame("Ide");
        frame.setSize(1150, 700);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        
        panel = new JPanel();
        panel.setLayout(null);
        panel.setBackground(Color.LIGHT_GRAY);
        frame.add(panel);
        
        cod();
        con();
        
        frame.setVisible(true);
        frame.setResizable(false);
   }
   
   void cod(){
        codigo = new JTextArea();
        codigo.setBounds(15, 65, 650, 450);
        panel.add(codigo);
        
        con = new JLabel("Consola");
        Font font = new Font("Agency FB", Font.BOLD, 14);
        con.setFont(font);
        panel.add(con);
        con.setBounds(15, 525, 65, 15);
        
   }
   
   void con(){
       consola = new JTextArea();
        consola.setBounds(15, 550, 1110, 100);
        panel.add(consola);
        
        cod = new JLabel("Código");
        Font font = new Font("Agency FB", Font.BOLD, 14);
        cod.setFont(font);
        panel.add(cod);
        cod.setBounds(15, 45, 65, 15);
   }
}
