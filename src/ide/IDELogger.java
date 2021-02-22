/**
 * Rodrigo Maafs | AppsCamelot 2021
 */
package ide;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.math.BigDecimal;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.nio.file.StandardOpenOption;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Rodrigo Maafs
 */
public class IDELogger {

    private static SimpleDateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
    private static File file = new File("log.txt");//Archivo donde guardaremos el log
    private static boolean showLog = true;

    public IDELogger() {
        //Si el archivo no existe, lo creamos
        if (!file.exists()) {
            try {
                file.createNewFile();
            } catch (IOException ex) {
                Logger.getLogger(Config.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    /**
     * Función para imprimir en el log algún mensaje
     *
     * @param msg Mensaje a imprimir
     */
    public static void log(String msg) {
        msg = dateFormat.format(new Date()) + " > " + msg;//Le añadimos prefix al mensaje
        if (showLog) {//Si está activado, mostramos los mensajes en pantalla.
            System.out.println(msg);
        }
        writeLine(msg);
    }

    /**
     * Función que escribe una nueva línea en el archivo del log.
     *
     * @param msg Mensaje a escribir.
     */
    private static void writeLine(String msg) {
        msg += "\n";//Añadimos un salto de línea al mensaje
        try {
            Files.write(Paths.get(file.getAbsolutePath()), msg.getBytes(), StandardOpenOption.APPEND);//Insertamos el mensaje
        } catch (IOException ex) {
            System.out.println("Error al escribir en el archivo log: " + ex.getMessage());
            ex.printStackTrace();
        }

        //Obtenemos el tamaño del archivo del log
        BigDecimal fileSizeMB = new BigDecimal((double) file.length() / 1024.00 / 1024.00);//Tamaño del archivo en MB
        if (fileSizeMB.doubleValue() > 3) {//Si el archivo es mayor a 5MB
            System.out.println("El archivo del log pesa " + fileSizeMB.setScale(2, BigDecimal.ROUND_HALF_EVEN) + " MB, restaurando archivo...");
            try {
                BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(file)));
                bw.write("");//Texto vació
                bw.close();
            } catch (Exception ex) {
                Logger.getLogger(IDELogger.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
}
