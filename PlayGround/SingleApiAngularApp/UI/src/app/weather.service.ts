import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { WeatherForecast } from './weather-forecast.model';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  private readonly apiUrl = 'http://localhost:5297/weatherforecast';

  constructor(private http: HttpClient) {}

  getForecast(): Observable<WeatherForecast[]> {
    return this.http.get<WeatherForecast[]>(this.apiUrl);
  }
}
