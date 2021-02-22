package ide;

import java.util.logging.Level;
import java.util.logging.Logger;

public class Ide {

    public static void main(String[] args) {
        new IDELogger();//Creamos la instancia para que se llenen los datos estáticos en el constructor

        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                try {
                    new Main().setVisible(true);
                } catch (Exception ex) {
                    Logger.getLogger(Main.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        });
    }

}
