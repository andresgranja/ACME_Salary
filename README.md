# PaymentSystem

Hi everyone, this in exercise sent to me by ioet EC, I had a great time solving it, and I hope mi solution complies with the requirements.

## The Problem

The company ACME offers their employees the flexibility to work the hours they want. They will pay for the hours worked based on the day of the week and time of day, according to the following table:

|Monday - Friday|Dollar/Hour|
|--------------|-------|
|00:01 - 09:00 | 25 USD|
|09:01 - 18:00 | 15 USD|
|18:01 - 00:00 | 20 USD|

|Saturday and Sunday|Dollar/Hour|
|--------------|-------|
|00:01 - 09:00 | 30 USD|
|09:01 - 18:00 | 20 USD|
|18:01 - 00:00 | 25 USD|

The goal of this exercise is to calculate the total that the company has to pay an employee, based on the hours they worked and the times during which they worked. The following abbreviations will be used for entering data:

|Code|Day|
|----|---|
|MO| Monday|
|TU| Tuesday|
|WE| Wednesday|
|TH| Thursday|
|FR| Friday|
|SA| Saturday|
|SU| Sunday|

Input: the name of an employee and the schedule they worked, indicating the time and hours. This should be a .txt file with at least five sets of data. You can include the data from our two examples below.

Output: indicate how much the employee has to be paid

For example:

Case 1:

INPUT

RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00

OUTPUT:

The amount to pay RENE is: 215 USD

Case 2:

INPUT

ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00

OUTPUT:

The amount to pay ASTRID is: 85 USD

## My solution

The first thing i did was to figure a way to compare time strings and get the actual range in which the employee worked, after solving that everything was easier, I could figure out how many hours the employee worked subtracting the final time and the initial time, and from then it was basic math.

## To run the code

```sh
Copy the project to c:\ACME
```
```sh
dotnet build
```
```sh
dotnet run --project ACME.Salary 
```

To change the inputs just edit employee_data.txt using the format of the exercise.

## To run the tests
```sh
dotnet test
```
