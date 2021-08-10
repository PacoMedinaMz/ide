package functions;

import java.io.InputStream;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.json.simple.parser.ContainerFactory;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

public class GOExecuter {

    private String outputE;
    private String outputLexico;
    private String outputSintac;
    private String outputSemant;
    private String outputCodigo;
    ExtraUtils f;
    private static AdminConfig fa;

    public GOExecuter() {
        fa = new AdminConfig();
        f = new ExtraUtils();
        outputE = "";
        outputSemant = "";
        outputLexico = "";
        outputSintac = "";
        outputCodigo = "";
    }

    public String conexion() {
        salida("\nCompilando...");
        ProcessBuilder pB = new ProcessBuilder();
        //Ejecutar y guardar Lexico del codigo
        try {
            executeCommand("go env -w GO111MODULE=off");
        } catch (Exception ex) {
            Logger.getLogger(GOExecuter.class.getName()).log(Level.SEVERE, null, ex);
            salidaE("ConexionException : " + ex.getMessage());
        }

        try {
            executeCommand("go run compiladorSintactico//main.go auxiliar.txt");
        } catch (Exception ex) {
            Logger.getLogger(GOExecuter.class.getName()).log(Level.SEVERE, null, ex);
            salidaE("ConexionException : " + ex.getMessage());
        }

        JSONParser parser = new JSONParser();
        String text = fa.leerTxt("compiladorSintactico//log//log.json");

        ContainerFactory containerFactory = new ContainerFactory() {
            @Override
            public Map createObjectContainer() {
                return new LinkedHashMap<>();
            }

            @Override
            public List creatArrayContainer() {
                return new LinkedList<>();
            }
        };

        try {
            ArrayList<String> json = new ArrayList<String>();
            Map map = (Map) parser.parse(text, containerFactory);
            for (Object name : map.values()) {
                json.add(name.toString().substring(1, (name.toString().length() - 1)));
            }

            String[] textLexico = json.get(0).split("(?=Token)");
            for (int i = 0; i < textLexico.length; i++) {
                outputLexico += textLexico[i].substring(0, textLexico[i].length() - 2) + "\n";
            }

            String[] textSem = json.get(2).split("(?=Agregado)|(?=Tabla)");
            for (int i = 0; i < textSem.length; i++) {
                outputSemant += textSem[i].substring(0, textSem[i].length() - 2) + "\n";
            }

            String[] textErrores = json.get(3).split("(?=E)");
            for (String textErrore : textErrores) {
                salida(textErrore);
            }

            String[] textCode = json.get(4).split("(?=asn)|(?=lab)|(?=lt)|(?=if_f)|(?=sub)|(?=goto)|(?=halt)|(?=rd)|(?=wri)|(?=div)|(?=mul)|(?=and)|(?=or)|(?=not)|(?=leq)|(?=gt)|(?=geq)|(?=eq)|(?=ineq)|(?=add)");
            for (int i = 0; i < textCode.length; i++) {
                if ((textCode[i].length() - 4) > 0) {
                    outputCodigo += textCode[i].substring(0, textCode[i].length() - 4) + "\n";
                }
            }

            String[] textArbol = json.get(5).split("(?=TokenType)|(?=Izq)|(?=Med)|(?=Der)|(?=Bro)");
            int es = 0;
            for (String textArbol1 : textArbol) {
                for (int j = 0; j < es; j++) {
                    outputSintac += "|    ";
                }
                outputSintac += textArbol1.substring(0, textArbol1.length() - 2) + "\n";
                char primero = textArbol1.charAt(0);
                int count = textArbol1.length() - textArbol1.replace("}", "").length();
                if (primero == 'I' || primero == 'M' || primero == 'D' || primero == 'B') {
                    es++;
                }
                if (count > 1) {
                    es += (1 - count);
                }
            }

        } catch (ParseException pe) {
            System.out.println("position: " + pe.getPosition());
            System.out.println(pe);
        }

        salida("\nCódigo comprobado.");
        return outputE;
    }

    private void executeCommand(String command) {
        try {
            log(command);
            Process process = Runtime.getRuntime().exec("cmd.exe /c " + command);
            logOutput(process.getInputStream(), "");
            logOutput(process.getErrorStream(), "Error: ");
            process.waitFor();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void logOutput(InputStream inputStream, String prefix) {
        new Thread(() -> {
            Scanner scanner = new Scanner(inputStream, "UTF-8");
            while (scanner.hasNextLine()) {
                synchronized (this) {
                    log(prefix + scanner.nextLine());
                }
            }
            scanner.close();
        }).start();
    }

    private static SimpleDateFormat format = new SimpleDateFormat("MM/dd/yyyy hh:mm:ss:SSS");

    private synchronized void log(String message) {
        System.out.println(format.format(new Date()) + ": " + message);
    }

    private void salidaE(String x) {
        outputE += "\n\n  == Exited with " + x + "\n";
    }

    private void salida(String x) {
        outputE += x + "\n";
    }

    private void br() {
        outputE += "\n";
    }

    public String getOutputE() {
        return outputE;
    }

    public String getOutputSemant() {
        return outputSemant;
    }

    public String getOutputLexico() {
        return outputLexico;
    }

    public String getOutputSintac() {
        return outputSintac;
    }

    public String getOutputCodigo() {
        return outputCodigo;
    }

}
