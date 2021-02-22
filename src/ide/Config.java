/**
 * Rodrigo Maafs | AppsCamelot 2020
 */
package ide;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.util.HashMap;
import java.util.Map;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Rodrigo Maafs
 */
public class Config {

    //En este enum, se guardarán todos las opciones importantes que se usarán en la configuración.
    public enum Opcion {
        LAST_FILE
    }

    private File file = new File("config.yml");//Archivo donde guardaremos la configuración
    private HashMap<String, String> config = new HashMap<>();//Configuraciones cargadas en RAM

    public Config() {
        //Si la configuración no existe, la creamos
        if (!file.exists()) {
            try {
                file.createNewFile();
            } catch (IOException ex) {
                Logger.getLogger(Config.class.getName()).log(Level.SEVERE, null, ex);
            }
        }

        //Añadimos en el hash, todas opciones importantes
        for (Opcion op : Opcion.values()) {
            config.put(op.name().toLowerCase(), "");
        }

        //Intentamos cargar todas las configuraciones guardadas
        try {
            load();
        } catch (Exception e) {
            System.out.println("Error al leer el archivo de configuración: " + e.getMessage());
            e.printStackTrace();
        }
    }

    /**
     * Función que obtiene una configuración.
     *
     * @param key Opción a obtener
     * @return Retorna el value de la opción
     */
    public String get(String key) {
        if (config.containsKey(key)) {
            return config.get(key);
        }
        return "";
    }

    /**
     * Función que obtiene una configuración usando el enum de opciones
     * importantes.
     *
     * @param key Opción a obtener
     * @return Retorna el value de la opción
     */
    public String get(Opcion key) {
        return get(key.name().toLowerCase());
    }

    /**
     * Función para guardar valores en la configuracion.
     *
     * @param key Opción a guardar
     * @param value Valor de la opción
     */
    public void set(String key, String value) {
        config.put(key, value);
        try {
            save();
        } catch (Exception e) {
            System.out.println("Error al guardar el archivo de configuración: " + e.getMessage());
            e.printStackTrace();
        }
    }

    /**
     * Función para guardar valores en la configuracion usando una configuración
     * importante.
     *
     * @param key Opción importante a guardar
     * @param value Valor de la opción
     */
    public void set(Opcion key, String value) {
        set(key.name().toLowerCase(), value);
    }

    /**
     * Función que leerá el archivo de configuración y obtendrá sus opciones.
     *
     * @throws Exception
     */
    private void load() throws Exception {
        BufferedReader reader = new BufferedReader(new FileReader(file));
        String line = "";
        while (line != null) {//Ciclamos todas las líneas del archivo
            line = reader.readLine();
            if (line == null) {
                continue;
            }
            line = line.trim();

            if (line.contains("=")) {
                config.put(line.split("=")[0], line.split("=")[1]);
            }
        }
        reader.close();
    }

    /**
     * Función que guardará todas las opciones en el archivo de configuración.
     *
     * @throws Exception
     */
    private void save() throws Exception {
        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(file)));
        StringBuilder str = new StringBuilder("");

        //Leemos todos los registros de la configuración y las grabamos en el archivo.
        for (Map.Entry reg : config.entrySet()) {
            str.append((String) reg.getKey()).append("=").append((String) reg.getValue()).append("\n");
        }

        bw.write(str.toString());
        bw.close();
        System.out.println("Configuración guardada.");
    }
}
