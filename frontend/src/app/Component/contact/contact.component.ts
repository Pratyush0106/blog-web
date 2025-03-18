import { Component,Inject,inject } from '@angular/core';
import { ContactService } from '../../Service/contact.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
 // Adjust the path as necessary

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
  host: { 'skiphydration': 'true' },
  imports: [FormsModule]
})
export class ContactComponent {
  name: string = '';
  email: string = '';
  message: string = '';
  router  = Inject(Router);

  constructor(private contactService: ContactService) {}

  onSubmit() {
    const contactData = {
      name: this.name,
      email: this.email,
      message: this.message
    };

    this.contactService.contactUs(contactData).subscribe(response => {
      alert('Message sent successfully');
      console.log('Contact form submitted successfully', response);
      this.router.navigate(['/blog']);
    }, error => {
      console.error('Error submitting contact form', error);
    });
  }
}