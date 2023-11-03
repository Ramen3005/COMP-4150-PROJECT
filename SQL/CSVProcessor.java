import java.io.*;
import java.util.stream.Collectors;

public class CSVProcessor {

    public static void main(String[] args) {

        String inputFilePath = "EPL_Matches.csv"; // Input CSV file path
        String outputFilePath = "output.csv"; // Output CSV file path

        try (BufferedReader br = new BufferedReader(new FileReader(inputFilePath));
             PrintWriter pw = new PrintWriter(new FileWriter(outputFilePath))) {

            String line;
            while ((line = br.readLine()) != null) {
                String modifiedLine = removeFirstValue(line);
                pw.println(modifiedLine);
            }

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static String removeFirstValue(String line) {
        return line.replaceFirst("^[^,]*,", "");
    }
}
