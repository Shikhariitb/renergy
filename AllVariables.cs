using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllVariables : MonoBehaviour{

    public static double total_power, instance_power, instance_cost, num_of_wind = 0;
    public static double[,] ind_pow = new double[8,17];
    public static bool[,] grid = new bool[8,17];
    public static int location, terrain_type;
    public static string[] city_terrain_info = new string[9] {"5.76","N","8","N","4.33", "6.73","6.18","3.88","N"};
    public static string[] constraints = new string[6] {"15MW","10000000","18MW","12000000","10MW","3000000"};
    public static double[] price = new double[3] {10000000,12000000,3000000};
}


    // string str = "print(rad)";
    // var py = Python.CreateEngine();
    // py.Execute(str);


    // public void calculate_final_output(){
    //     if(!AllVariables.grid[a + 1,b + 1]){
    //         double factor = 1;
    //         if(AllVariables.grid[a,b + 1]){
    //             factor *= 0.2;
    //         }
    //         if(AllVariables.grid[a + 1,b + 2]){
    //             factor *= 0.4;
    //         }
    //         if(AllVariables.grid[a + 1,b]){
    //             factor *= 0.4;
    //         }
    //         if(AllVariables.grid[a,b + 2]){
    //             factor *= 0.8;
    //         }
    //         if(AllVariables.grid[a,b]){
    //             factor *= 0.8;
    //         }
    //         total_output_display.text = (AllVariables.total_power + factor*AllVariables.instance_power).ToString();
    //     }
    // }

    // using IronPython.Hosting;

// namespace PrintToConsole
// {
//     internal class Program
//     {
//         private static void Main()
//         {
//             Console.WriteLine("What would you like to print from python?");
//             var input = Console.ReadLine();

//             var py = Python.CreateEngine();
//             try
//             {
//                 py.Execute("print('From Python: " + input + "')");
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine(
//                    "Oops! We couldn't print your message because of an exception: " + ex.Message);
//             }

//             Console.WriteLine("Press enter to exit...");
//             Console.ReadLine();
//         }
//     }
// }