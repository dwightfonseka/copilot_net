// project overview: create a C# console app that does statical calculations from user input
// this project consists of 9 parts (part A, partB, partC, partD, partE, partF, partG, partH, partI)
// input : user input a number in the console in part A
// output : the sum, average or random numbers generated displayed in part D
// implement part I for part A only
// implement (part E, part F ,partG and partH) for (part A, part B and part C only) and not other parts

// partA : random number generator
// create random numbers from 50 to 100 and display the numbers
// the number of random numbers generated is from the user input

// partB : sum of numbers
// dependency: partA
// calculate the sum of the random numbers generated in partA
// display the sum of the random numbers

// partC : average of numbers
// dependency: partA
// calculate the average of the random numbers generated in partA
// display the average of the random numbers

// partD: razor pages web app to display results (a separate file)
// dependency: partA, partB, partC
// display the results from partA, partB, partC in a web app

// partE:  optimization for latency and performance
// refactor the code to improve performance and reduce latency
// use async and await to improve performance
// use caching to reduce latency
// use dependency injection to improve performance
// algorithm optimization to improve performance

// partF:  error handling
// implement error handling in the code
// use try catch block to handle exceptions
// use logging to log exceptions
// use custom exceptions to handle errors
// use exception filters to handle errors
// use global exception handling to handle errors
// input validation to handle errors

// part G: logging and records (a separate file)
// log the results from partA, partB, partC

// part H: documentation
// documentation of all parts for explainbility
// master documentation of the project for explainability

// part I: security and authentication
// user password authentication for "password" to access the app
// maximum number of attempts to access the app for wrong password is set to 3
// rate limiting so user can only access the app 3 times in 1 minute