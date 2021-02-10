package ide;

import java.awt.Color;
import static java.lang.System.out;
import javax.swing.JFrame;
import javax.swing.JPanel;

public class Menu {
    private JFrame frame;
    private JPanel panel;
    
   Menu(){
        out.println("Hola");
        frame = new JFrame("Ide");
        frame.setSize(1150, 700);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        
        panel = new JPanel();
        panel.setLayout(null);
        panel.setBackground(Color.LIGHT_GRAY);
        frame.add(panel);
        frame.setVisible(true);
   }
    
}
