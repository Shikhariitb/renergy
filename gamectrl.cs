using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class gamectrl : MonoBehaviour{
    
    public Slider hslider;
    public Slider rslider;    
    public Slider twslider;
    public Slider taslider;
    public Button[] barray = new Button[90];
    public TextMeshProUGUI hinfo;
    public TextMeshProUGUI pinfo;
    public TextMeshProUGUI oinfo;
    public TextMeshProUGUI hvalue;
    public TextMeshProUGUI rvalue;
    public TextMeshProUGUI tavalue;
    public TextMeshProUGUI twvalue;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI instance_output_display;
    public TextMeshProUGUI total_output_display;
    public TextMeshProUGUI objective;
    public TextMeshProUGUI limit;
    public TextMeshProUGUI count;
    public TextMeshProUGUI target;
    int ter_t = 0;
    int loc, a = 0, b = 0;
    int afshape = 0;
    double ip, factor, f1, f2, f3, f4, f5, tcost = 102800;
    double rad = 20;
    double hgt = 60;
    double twi = 0;
    double tap = -0.125;
    int sel_tile = 0;
    bool terrain_valid = false;
    double power;

    public void onClick(int val){
        sel_tile = val;
        a = sel_tile/15;
        b = sel_tile%15;
        double lid = AllVariables.price[AllVariables.location]/AllVariables.instance_cost;
        int li = (int) lid;
        limit.text = li.ToString();
        target.text = AllVariables.constraints[2*AllVariables.location];
        for(int i = 0; i < 90; i++){
            barray[i].interactable = !AllVariables.grid[i/15 + 1,i%15 + 1];
        }
        if(!AllVariables.grid[a + 1,b + 1]){
            factor = 1;
            f1 = 1;
            f2 = 1;
            f3 = 1;
            f4 = 1;
            f5 = 1;
            power = 0;
            if(AllVariables.grid[a,b + 1]){
                factor *= 0.2;
            }
            if(AllVariables.grid[a + 2,b + 1]){
                f5 = 0.2;
            }
            if(AllVariables.grid[a + 1,b + 2]){
                factor *= 0.4;
                f1 = 0.4;
            }
            if(AllVariables.grid[a + 1,b]){
                factor *= 0.4;
                f2 = 0.4;
            }
            if(AllVariables.grid[a,b + 2]){
                factor *= 0.8;
            }
            if(AllVariables.grid[a,b]){
                factor *= 0.8;
            }
            if(AllVariables.grid[a + 2,b + 2]){
                f3 = 0.8;
            }
            if(AllVariables.grid[a + 2,b]){
                f4 = 0.8;
            }
            for(int i = 0; i < 90; i++){
                power += AllVariables.ind_pow[i/15 + 1,i%15 + 1];
            }
            power = power - ((1 - f5)*AllVariables.ind_pow[a + 2,b + 1] + (1 - f1)*AllVariables.ind_pow[a + 1,b + 2] + (1 - f2)*AllVariables.ind_pow[a + 1,b] + (1 - f3)*AllVariables.ind_pow[a + 2,b + 2] + (1 - f4)*AllVariables.ind_pow[a + 2,b]);
            total_output_display.text = (power + factor*AllVariables.instance_power).ToString() + " MW";
            instance_output_display.text = (factor*AllVariables.instance_power).ToString() + " MW";
        }
    }

    public void place(){
        if(!AllVariables.grid[a + 1,b + 1]){
            barray[sel_tile].interactable = false;
            AllVariables.grid[a + 1,b + 1] = true;
            AllVariables.ind_pow[a + 1,b + 1] = factor*AllVariables.instance_power;
            AllVariables.ind_pow[a + 1,b + 2] *= f1;
            AllVariables.ind_pow[a + 1,b] *= f2;
            AllVariables.ind_pow[a + 2,b + 2] *= f3;
            AllVariables.ind_pow[a + 2,b] *= f4;
            AllVariables.ind_pow[a + 2,b + 1] *= f5;
            AllVariables.total_power = power + factor*AllVariables.instance_power;
            AllVariables.num_of_wind += 1;
            count.text = AllVariables.num_of_wind.ToString();
        }   
    }

    public void calculate_output(){
        if(afshape == 0){
            if(rad == 50 && twi == 10){
                ip = 0.5;
            }
            else if(rad == 60 && twi == 7){
                ip = 1.08;
            }
        }
        else if(afshape == 2){
            if(rad == 50 && twi == 18){
                ip = 0.43;
            }
        }
        instance_output_display.text = ip.ToString() + " MW";
    }

    public void hSelect(){
        ter_t = 0;
        AllVariables.terrain_type = ter_t;
        if(AllVariables.city_terrain_info[3*AllVariables.location] != "N"){
            hinfo.text = "Wind Speed = " + AllVariables.city_terrain_info[3*AllVariables.location] + '\n' + "Maintenance and Building cost medium";
            terrain_valid = true;
        }
        else{
            hinfo.text = "Terrain not present in city";
            terrain_valid = false;
        }
        pinfo.text = " ";
        oinfo.text = " ";
    }   

    public void pSelect(){
        ter_t = 1;
        AllVariables.terrain_type = ter_t;
        hinfo.text = " ";
        if(AllVariables.city_terrain_info[3*AllVariables.location + 1] != "N"){
            pinfo.text = "Wind Speed = " + AllVariables.city_terrain_info[3*AllVariables.location + 1] + '\n' + "Maintenance and Building cost low";
            terrain_valid = true;
        }
        else{
            pinfo.text = "Terrain not present in city";
            terrain_valid = false;
        }
        oinfo.text = " ";
    }   

    public void oSelect(){
        ter_t = 2;
        AllVariables.terrain_type = ter_t;
        hinfo.text = " ";
        pinfo.text = " ";
        if(AllVariables.city_terrain_info[3*AllVariables.location + 2] != "N"){
            oinfo.text = "Wind Speed = " + AllVariables.city_terrain_info[3*AllVariables.location + 2] + '\n' + "Maintenance and Building cost high";
            terrain_valid = true;
        }
        else{
            oinfo.text = "Terrain not present in city";
            terrain_valid = false;
        }
    }

    public void sel_loc(int val){
        loc = val;
        AllVariables.location = loc;
    }

    public void sel_afs(int val){
        afshape = val;
        print(val);
    }

    public void height(){
        hgt = hslider.value;
        if(AllVariables.terrain_type == 0){
            tcost = 0.95*rad*rad*rad + 19.5*hgt*hgt + 40000;
        }
        else if(AllVariables.terrain_type == 1){
            tcost = 0.95*rad*rad*rad + 19.5*hgt*hgt + 25000;
        }
        else{
            tcost = 0.95*rad*rad*rad + 19.5*hgt*hgt + 60000;
        }
        hvalue.text = "Height = " + hgt.ToString();
        cost.text = "Cost = " + tcost.ToString(); 
    }
    
    public void radius(){
        rad = rslider.value;
        if(AllVariables.terrain_type == 0){
            tcost = 0.95*rad*rad*rad + 19.5*hgt*hgt + 40000;
        }
        else if(AllVariables.terrain_type == 1){
            tcost = 0.95*rad*rad*rad + 19.5*hgt*hgt + 25000;
        }
        else{
            tcost = 0.95*rad*rad*rad + 19.5*hgt*hgt + 60000;
        }
        rvalue.text = "Radius = " + rad.ToString(); 
        cost.text = "Cost = " + tcost.ToString(); 
    }

    public void twist(){
        twi = twslider.value;
        twvalue.text = "Twist = " + (twi/10).ToString();
    }

    public void taper(){
        tap = taslider.value;
        tavalue.text = "Taper = " + (tap/200).ToString();
    }

    public void show_obj(){
        objective.text = "Required power = " + AllVariables.constraints[2*AllVariables.location] + '\n' + "Budget = " + AllVariables.constraints[2*AllVariables.location + 1];
    }   
        
    public void toMenu(){
        SceneManager.LoadScene(0);
    }

    public void toWind1(){
        SceneManager.LoadScene(1);
    }
 
    public void toWind2(){
        SceneManager.LoadScene(2);
    }
 
    public void toWind3(){
        if(terrain_valid){
            SceneManager.LoadScene(3);
        }        
    }
 
    public void toWind4(){
        SceneManager.LoadScene(4);
        AllVariables.instance_cost = tcost;
        AllVariables.instance_power = ip;
    }
}