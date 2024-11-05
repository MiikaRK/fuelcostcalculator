# FuelCostCalculator 
© Miika Rinta-Korkeamäki (2024) \
\
    <a href="https://github.com/MiikaRK/fuelcostcalculator/releases/latest">
        <img src="https://img.shields.io/github/v/release/MiikaRK/fuelcostcalculator?color=green" alt="latest release"></a>
[![FuelCostCalculator](https://github.com/MiikaRK/fuelcostcalculator/actions/workflows/FuelCostCalculator.yml/badge.svg?branch=master)](https://github.com/MiikaRK/fuelcostcalculator/actions/workflows/FuelCostCalculator.yml?query=branch%3Amaster)
[![CodeQL](https://github.com/MiikaRK/fuelcostcalculator/actions/workflows/github-code-scanning/codeql/badge.svg?branch=master)](https://github.com/MiikaRK/fuelcostcalculator/actions/workflows/github-code-scanning/codeql?query=branch%3Amaster)
![GitHub top language](https://img.shields.io/github/languages/top/MiikaRK/fuelcostcalculator)
[![License](https://img.shields.io/badge/License-MIT-blue)](#license)

![image](https://github.com/user-attachments/assets/b1c9787a-fcf2-4366-82c6-1602d995c290)

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

![Screenshot_2024-11-05-10-27-12-99_d4be62f4caa066d8efec4b5bd04fe7a4](https://github.com/user-attachments/assets/fe9af2c0-de0a-4429-84da-0d1af06829fd)
![Screenshot_2024-11-05-10-27-27-06_d4be62f4caa066d8efec4b5bd04fe7a4](https://github.com/user-attachments/assets/89df7c1e-9e5f-471f-9125-415a2ef8ef1c)
![Screenshot_2024-11-05-10-27-58-77_d4be62f4caa066d8efec4b5bd04fe7a4](https://github.com/user-attachments/assets/4ed73ecf-3581-4605-9450-6d244017935b)
![Screenshot_2024-11-05-10-28-07-17_d4be62f4caa066d8efec4b5bd04fe7a4](https://github.com/user-attachments/assets/b39f8516-2c9c-43dc-ac81-55c5de411857)
![Screenshot_2024-11-05-10-28-16-53_d4be62f4caa066d8efec4b5bd04fe7a4](https://github.com/user-attachments/assets/5419316c-72aa-40e6-bbac-5f22733f09be)

## License

Released under [MIT](/LICENSE) by [@MiikaRK](https://github.com/MiikaRK).
