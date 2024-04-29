import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetProductCategory } from '../../models/ProductCategory';
import { MatDialogRef } from '@angular/material/dialog';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrl: './product-create.component.scss'
})
export class ProductCreateComponent implements OnInit {

  form: FormGroup;
  categories: GetProductCategory[] = [];

  constructor(
    public dialogRef: MatDialogRef<ProductCreateComponent>,
    private fb: FormBuilder,
    private productService: ProductService
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0.01)]],
      description: [''],
      productCategoryId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadProductCategories();
  }

  onCreate(): void {
    if (this.form.valid) {
      const product = this.form.value;
      this.productService.createProduct(product).subscribe({
        next: data => this.dialogRef.close(data)
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  private loadProductCategories(): void {
    this.productService.getProductCategories().subscribe({
      next: data => this.onLoadProductCategories(data)
    })
  }

  private onLoadProductCategories(data: GetProductCategory[]): void {
    this.categories = data;
  }

}
