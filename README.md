# Dog Breeds And Weather

This is a test assignment solution for a Unity application with two main features:
- **Weather Tab:** Automatically fetches and displays weather data from ([weather.gov](https://weather.gov/)).
- **Dog Breeds Tab:** Fetches and displays 10 random dog breeds from the new Dog API v2 ([dogapi.dog](https://dogapi.dog/docs/api-v2)), with pop-ups for breed details.

## Technologies
- **Unity 2022.3+**
- **Zenject** for Dependency Injection
- **Newtonsoft.Json** for JSON parsing

## Features
- Two main tabs (Weather and Dog Breeds) with dynamic UI updates.
- Asynchronous HTTP requests managed via a custom API Request Queue.
- Dynamic creation of UI elements using Zenject Factory.
- Adaptive UI layout with pop-ups and loading indicators.
