# Problem statement

The Israeli Mossad, due to its special recruitment methods, has access to all developers in the world.  
Their cyber units are rapidly growing, and the recruitment process must be improved to stand their needs.  
The challenge is to filter out and find the best matching candidates for the right position.  
Your task is to craft application, that will make their work more efficient.
Preferably Xamarin Android application, but if that is not your area please feel free to use any other .net project (console, web, windows)

### The Mossad recruiter is expecting to be able to:
1. Insert a match criteria - a combination of a technology (from a list) and years of experience (a number)
2. If you choose Xamarin than swipe left or right on a candidate, which will mark him/her as accepted or rejected, otherwise try to be as creative as possible
3. View a list of all accepted candidates

### Data from the scouting department
Get technologies:  
https://app.ifs.aero/EternalBlue/api/technologies  
Get candidates:  
https://app.ifs.aero/EternalBlue/api/candidates

### A few things to notice
1. The Mossad doesn't give second chances. Regardless of accepting or rejecting a candidate, he/she should never be shown again
2. Against all best practices, login/auth isn't part of the task
3. No need to model all the data from the endpoints, please choose only the fields you like - keep it simple
4. The solution will be tested and it is expected to run without any extra steps necessary
5. Please don't spend more than 4 hours on this task, it's alright if you don't complete it  
  
<br/>
<br/>

#   Solution 

has two parts. Angular front end and .net api back end.

-- to run angular
npm install
ng serve

-- to run .net api
dotnet run
