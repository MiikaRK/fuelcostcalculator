# FuelCostCalculator 
© Miika Rinta-Korkeamäki (2024) \
\
    <a href="https://github.com/MiikaRK/fuelcostcalculator/releases/latest">
        <img src="https://img.shields.io/github/v/release/MiikaRK/fuelcostcalculator?color=green" alt="latest release"></a>
[![FuelCostCalculator](https://github.com/MiikaRK/fuelcostcalculator/actions/workflows/FuelCostCalculator.yml/badge.svg?branch=master)](https://github.com/MiikaRK/fuelcostcalculator/actions/workflows/FuelCostCalculator.yml?query=branch%3Amaster)
[![CodeQL](https://github.com/MiikaRK/fuelcostcalculator/actions/workflows/github-code-scanning/codeql/badge.svg?branch=master)](https://github.com/MiikaRK/fuelcostcalculator/actions/workflows/github-code-scanning/codeql?query=branch%3Amaster)
![GitHub top language](https://img.shields.io/github/languages/top/MiikaRK/fuelcostcalculator)
[![License](https://img.shields.io/badge/License-MIT-blue)](#license)



![image](https://github.com/MiikaRK/fuelcostcalculator/assets/94705211/14cb29cd-5a67-48d7-9e75-ac0e7f415cec) 


## Overview

Simple fuel cost calculator built with .NET MAUI framework and C#. At the moment, this program can be used in **Android** devices.

## Used technologies

- .NET MAUI
- C#
- SQLite

## Prerequisites

- Android Device (requires Android 5.0 or newer version)

## Features

- Calculator takes following inputs from the user and calculates fuel cost as a price (€): 
  - Travelled distance (km)
  - Average fuel consumption (l/100km)
  - Current fuel price (€/l) (current average fuel price in Finland)
  - The number of the people to share the fuel costs (optional)
 
- Recent calculations are saved to the database in the device and can be seen in History page

## Screenshots

![FuelCostCalculator_1](https://github.com/user-attachments/assets/2765ba74-046f-4d0d-bd1c-0ab0048a4742)
![FuelCostCalculator_2](https://github.com/user-attachments/assets/55a54ed4-a678-4e0a-a80d-94270c700569)
![FuelCostCalculator_3](https://github.com/user-attachments/assets/7a0ed832-8b70-4056-af93-68b10ac68888)
![FuelCostCalculator_4](https://github.com/user-attachments/assets/e96ffb12-7354-4126-ade1-3af28cb28719)
![FuelCostCalculator_5](https://github.com/user-attachments/assets/2e6d57e8-d6c7-460e-a2a6-494519ffb91c)

## License

Released under [MIT](/LICENSE) by [@MiikaRK](https://github.com/MiikaRK).
