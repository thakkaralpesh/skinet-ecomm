import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-error',
  standalone: true,
  imports: [MatButton],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {
baseUrl="https://localhost:5001/api";
private http=inject(HttpClient);

get404Error(){
  
}
}
