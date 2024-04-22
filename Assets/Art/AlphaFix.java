package alphaFix;
import java.awt.image.BufferedImage;
import java.awt.image.DataBufferByte;
import java.io.File;
import javax.imageio.ImageIO;

public class AlphaFix {

    public static void main(String[] args) {
        strings = ['Assets/Art/RetouchedForestArtSprites/Background_Trees_Touched_Up.png', 'Assets/Art/RetouchedForestArtSprites/Ground_Rocks_Touched_Up.png', 'Assets/Art/RetouchedForestArtSprites/Light_Green_Trees_Touched_Up.png', 'Assets/Art/RetouchedForestArtSprites/Trees_Leaves_Touched_Up.png'];
        // String inputImagePath = "/Users/iancho/Desktop/createdAssetsGameDesign/alphaTest.png"
        // 		+ ""; // Replace this with the path to your image
        // String outputImagePath = "/Users/iancho/Desktop/createdAssetsGameDesign/alphaResult.png"; // Output path for modified image
        
        for (int i = 0; i < strings.length; i++) {
        	String inputImagePath = strings[i];
        	String outputImagePath = strings[i];
        	// Call the fixAlpha method
        	fixAlpha(inputImagePath, outputImagePath);
        }

        def fixAlpha(String inputImagePath, String outputImagePath) {
            try {
                // Read the image
                BufferedImage image = ImageIO.read(new File(inputImagePath));
                
                // Loop through each pixel and change alpha value to 255 if it's not 0
                for (int y = 0; y < image.getHeight(); y++) {
                    for (int x = 0; x < image.getWidth(); x++) {
                        int argb = image.getRGB(x, y);
                        int alpha = (argb >> 24) & 0xFF;
                        if (alpha != 0) {
                            argb = (argb & 0x00FFFFFF) | (255 << 24);
                            image.setRGB(x, y, argb);
                        }
                    }
                }
                
                // Save the modified image
                ImageIO.write(image, "png", new File(outputImagePath));
                System.out.println("Image saved as " + outputImagePath);
                
            } catch (Exception e) {
                e.printStackTrace();
            }
        }


        // try {
        //     // Read the image
        //     BufferedImage image = ImageIO.read(new File(inputImagePath));
            
        //     // Loop through each pixel and change alpha value to 255 if it's not 0
        //     for (int y = 0; y < image.getHeight(); y++) {
        //         for (int x = 0; x < image.getWidth(); x++) {
        //             int argb = image.getRGB(x, y);
        //             int alpha = (argb >> 24) & 0xFF;
        //             if (alpha != 0) {
        //                 argb = (argb & 0x00FFFFFF) | (255 << 24);
        //                 image.setRGB(x, y, argb);
        //             }
        //         }
        //     }
            
        //     // Save the modified image
        //     ImageIO.write(image, "png", new File(outputImagePath));
        //     System.out.println("Image saved as " + outputImagePath);
            
        // } catch (Exception e) {
        //     e.printStackTrace();
        // }
    }
}