import { Component } from "@angular/core";
import { FormsModule} from "@angular/forms";

@Component({
    selector: "hello-app",
    imports: [FormsModule],
    template: `<h2>Login:</h2>
               <input [(ngModel)]="name" placeholder="name">
               <h3>Hello, {{name}}!</h3>`
})
export class AppComponent {
    name = "";
}