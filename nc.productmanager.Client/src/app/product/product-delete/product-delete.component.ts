import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { GetProduct, Product } from '../../models/Product';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-delete',
  templateUrl: './product-delete.component.html',
  styleUrl: './product-delete.component.scss'
})
export class ProductDeleteComponent {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: GetProduct,
    private productService: ProductService,
    private dialogRef: MatDialogRef<ProductDeleteComponent>
  ) {}

  deleteProduct(): void {
    if(this.data.id) {
      this.productService.deleteProduct(this.data.id).subscribe({
        next: _ => this.onDeleteProduct()
      });
    }
  }

  private onDeleteProduct(): void {
    this.dialogRef.close();
  }

}
