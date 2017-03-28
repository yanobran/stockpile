# Getting Started:
  - Install NodeJS (Version 6 or up) & Node Package Manager (NPM)
  - Globally install gulp via node package manager:

```sh
$ npm install -g gulp
```


- CD into the 'StockPile.Presentation.Web' project directory and run NPM install to install all required packages in the package.json configuration file. 
- After the packages are installed, build the client-side application using npm run build as per below:

```sh
$ cd StockPile.Presentation.Web
$ npm install
$ npm run build
```


# Running the Application:
- Open the Visual Studio solution and restore nuget packages
- Build the solution
- The solution follows the structure indicated in the table below:

| Folder        | Endpoint Project                      | Endpoint  |
| ------------- |---------------------------------------| ----------|
| API           | StockPile.Api                         | http://localhost:57339/ |
| Presentation  | StockPile.Presentation.Web            | http://localhost:58413/ |
| App Services  | StockPile.Services.ApplicationService | http://localhost:58596/ |

- For each of the above applications, open the "_implementation" folder under the solution folder that corresponds to the 'Folder' name column above.
- Right-click and debug each one of these endpoint applications.
- When the above endpoints are up and running, navigate to the Presentation URL and the application should fire up.


# Application Services:
Try some of the follow queries in your browser and you should see JSON data output in your browser:
- http://localhost:58596/inventory/products
- http://localhost:58596/inventory/brands
- http://localhost:58596/inventory/categories

# API:
Similarily try out some queries on the API endpoint (NOTE - the Application Service must be running):
- http://localhost:57339/api/inventory/product/3763a3bf-8c00-4971-8389-aae4a98299c6
- http://localhost:57339/api/inventory/categories
- http://localhost:57339/api/inventory/brands

# Presentation:
The client side React/Mobx app is house under the '_src' folder in the StockPile.Presentation.Web project. These scripts get transpiled and bundled into the wwwroot/js folder so please remember the 'npm run build' step for this project before launching the site.

### Tips/Client-side Design:

- If planning to develop with React and JSX, I would recommend using VS Code. Not only is the syntax highlighting much better than VS2017, but you can leave your VS debugging while working outside of VS on your client-side scripts.


- The entire client side state is managed in the `_src/store` folder. The client side state store uses Mobx to manage the state as a set of observables and actions. React classes can simply inject the store and observe the data using an ES7 decorator `@inject('inventoryStore') @observer class Search extends React.Component {` 


- The `mobx-react` NPM module provides a class called `Provider` that we use to pass the state into the React Router in the app.jsx file `<Provider inventoryStore={inventoryStore}>...`


- The design goal was to make the client-side store and API client `_src/services/apiclient.js` completely reusable so that you could easily port it over to an Angular or other type of SPA application. The store and api client have absolutely no dependencies on React.
