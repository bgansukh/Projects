package cs1050Assignments;

import java.io.PrintWriter;
import java.io.File;
import java.io.FileNotFoundException;

public class Gym {

	int maxAthletes = 3;
	private String gymName;
	private Athlete[] athletes;
	private int numAthletes;

	public Gym(String name, int maxAthletes) {
		this.gymName = name;
		this.athletes = new Athlete[maxAthletes];
		this.numAthletes = 0;
	}

	public void addAthlete(Athlete a) {
		if (numAthletes == maxAthletes) {
			System.out.println("Gym is full. Can't add Infinite Loop");
			return;
		}

		if (numAthletes < athletes.length) {
			athletes[numAthletes] = a;
			numAthletes++;
		}
	}

	public void displayAthleteSummaries() {
		System.out.println("\n****** Gym Fitness Report ******");
		System.out.println("Gym: " + getGymName());
		for (int i = 0; i < numAthletes; i++) {
			Athlete a = athletes[i];
			System.out.println("Athlete: " + a.getName());
			System.out.println("       Max Heart Rate: " + a.calculateMaxHeartRate() + " bpm");
			System.out.printf("       Average Daily Calories Burned: %.1f%n", a.getAverageCalories());
			System.out.printf("       BMI: %.1f Category: %s%n", a.calculateBMI(), a.getBMICategory());
// \n new line character(puts in new line without need for formatter)
// %.1f = sets whatever number is mapped to it to decimal value (1 decimal point)
// %s sets whatever string its mapped to it, to string after comma
// %n new line character, best practice is to use in string formatting 

		}
	}

	public void saveReportToFile() throws FileNotFoundException {
		String filename = getGymName() + "_Report.txt";

		File fitnessReportFile = new File(filename);

		if (fitnessReportFile.exists()) {
			System.out.println("\nThe file Elite Fitness_Report.txt already exists");
		} else {
			PrintWriter thePrintWriter = new PrintWriter(fitnessReportFile);
			thePrintWriter.println("****** Gym Fitness Report ******");
			for (int i = 0; i < numAthletes; i++) {
				thePrintWriter.println("Athlete: " + athletes[i].getName());

				int curMaxHR = 220 - athletes[i].getAge();
				thePrintWriter.println("Max Heart Rate: " + curMaxHR + " bpm");

				thePrintWriter.printf("Average Daily Calories Burned: %.1f%n", athletes[i].getAverageCalories());

				thePrintWriter.printf("BMI: %.1f", athletes[i].calculateBMI());
				thePrintWriter.println(" Category: " + athletes[i].getBMICategory());
			}

			thePrintWriter.close();

		}

		printTopAthleteCalBurn();
		displayUnderweight();

		System.out.println("Report saved to:");
		System.out.println(fitnessReportFile.getAbsolutePath());
	}

	public void printTopAthleteCalBurn() {
		int top = 0;
		for (int i = 1; i < numAthletes; i++) {
			if (athletes[i].getAverageCalories() > athletes[top].getAverageCalories()) {
				top = i;
			}
		}

		Athlete topAthlete = getAthletes()[top];
		System.out.println("Top Athlete (Most Calories Burned): " + topAthlete.getName());
	}

	public void displayUnderweight() {
		System.out.println("Underweight Athletes:");
		boolean found = false;
		for (int i = 0; i < numAthletes; i++) {
			Athlete a = athletes[i];
			if (a.calculateBMI() < 18.5) {
				System.out.printf("- %s (BMI: %.1f)%n", a.getName(), a.calculateBMI());
				found = true;
			}
		}
		if (!found) {
			System.out.println("No underweight athletes.");

		}

	}

	/* Start Getters/Setters */
	public int getMaxAthletes() {
		return maxAthletes;
	}

	public void setMaxAthletes(int maxAthletes) {
		this.maxAthletes = maxAthletes;
	}

	public String getGymName() {
		return gymName;
	}

	public void setGymName(String gymName) {
		this.gymName = gymName;
	}

	public Athlete[] getAthletes() {
		return athletes;
	}

	public void setAthletes(Athlete[] athletes) {
		this.athletes = athletes;
	}

	public int getNumAthletes() {
		return numAthletes;
	}

	public void setNumAthletes(int numAthletes) {
		this.numAthletes = numAthletes;
	}
}