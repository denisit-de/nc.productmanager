import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-category-create',
  templateUrl: './product-category-create.component.html',
  styleUrl: './product-category-create.component.scss'
})
export class ProductCategoryCreateComponent {
  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<ProductCategoryCreateComponent>,
    private fb: FormBuilder,
    private productService: ProductService
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required]
    });
  }

  onCreate(): void {
    if (this.form.valid) {
      const product = this.form.value;
      this.productService.createProductCategories(product).subscribe({
        next: data => this.dialogRef.close(data)
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
