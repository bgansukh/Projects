package cs1050Assignments;

public class Athlete {

    // Fields
    private String name;
    private double height;
    private double weight;
    private int age;
    private double[] caloriesPerDay;
    
    // Constructor
    public Athlete(String name, double height, double weight, int age, double[] caloriesPerDay) {
        this.name = name;
        this.height = height;
        this.weight = weight;
        this.age = age;
        this.caloriesPerDay = caloriesPerDay;
    }

    // Getters
    public String getName() {
        return name;
    }

    public double getHeight() {
        return height;
    }

    public double getWeight() {
        return weight;
    }

    public int getAge() {
        return age;
    }

    public double[] getCaloriesPerDay() {
        return caloriesPerDay;
    }

    // Calculate average daily calories burned
    public double getAverageCalories() {
        double total = 0;
        for (int i = 0; i < caloriesPerDay.length; i++) {
            total += caloriesPerDay[i];
        }
        return total / caloriesPerDay.length;
    }

    // Calculate BMI
    public double calculateBMI() {
        return  weight / (height * height) * 703;
    }

    // Return max heart rate
    public int calculateMaxHeartRate() {
        return 220 - age;
    }

    // Return BMI category
    public String getBMICategory() {
        double bmi = calculateBMI();
        if (bmi < 18.5) {
            return "Underweight";
        } else if (bmi < 25) {
            return "Normal";
        } else if (bmi < 30) {
            return "Overweight";
        } else {
            return "Obese";
        }
    }
}
