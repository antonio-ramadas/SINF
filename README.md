# Sales Force Automation
Project developed on the course unit Informations Systems.

## Directories
### 1. Reports
#### 1.1 Specification
Includes the Project Overview, Functionalities/Features, Information Architecture and Interoperability with Primavera.
### 2. Web
Webpage developed using [Angular2](https://angular.io/) because it provides a great and easy way to make a single page application.
#### 2.1 Preequisites
Angular2 requires [Node.js](https://nodejs.org/en/) and [npm](https://www.npmjs.com/).
#### 2.2 Installation
Using npm from the command line on the web folder directory, install the packages with the command:
> npm install

#### 2.3 Deployment
Again, using npm from the command line on the web folder directory, run the command:
> npm start

If it doesn't automatically opens a tab on your browser, then manually enter http://localhost:3000/ on a browser of your choice.

**Note**: If in debug mode an exception is thrown, then edit your local repository on *zone.js/dist/zone.js* from `fetchPromise = global['fetch']();` to `fetchPromise = global['fetch']('');`
This is a known issue discussed [here](https://github.com/angular/zone.js/issues/436) and the solution shown is a workaround. Notice that this only happens when debugging. Normal use of the website does not require any changes.
### 3. Enterprise Resource Planner
