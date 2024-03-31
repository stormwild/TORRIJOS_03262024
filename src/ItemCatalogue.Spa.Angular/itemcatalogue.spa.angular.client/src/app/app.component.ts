import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CatalogueItems } from '../types';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  public catalogue: CatalogueItems | undefined;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );

    this.http.get<CatalogueItems>('/catalogue').subscribe(
      (result) => {
        console.log('result', result)
        this.catalogue = result;
      },
      (error) => {
        console.error(error);
      }
    )
  }

  title = 'itemcatalogue.spa.angular.client';
}
