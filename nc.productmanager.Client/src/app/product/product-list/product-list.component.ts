import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { GetProduct } from '../../models/Product';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { ProductService } from '../product.service';
import { MatDialog } from '@angular/material/dialog';
import { ProductCreateComponent } from '../product-create/product-create.component';
import { ProductDeleteComponent } from '../product-delete/product-delete.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {

  dataSource = new MatTableDataSource<GetProduct>();
  searchTerm: string = '';

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = [
    'name',
    'price',
    'description',
    'category',
    'actions'
  ];

  constructor(
    private productService: ProductService,
    private dialog: MatDialog) {}

  ngOnInit(): void {
    this.initProducts();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openCreateProductDialog(): void {
    const dialogRef = this.dialog.open(ProductCreateComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.initProducts();
      }
    });
  }

  openDeleteProductDialog(product: GetProduct): void {
    const dialogRef = this.dialog.open(ProductDeleteComponent, {
      data: product
    });

    dialogRef.afterClosed().subscribe(_ => {
      this.initProducts();
    });
  }

  private initProducts(): void {
    this.productService.getProducts().subscribe({
      next: data => this.onGetProducts(data)
    });
  }

  private onGetProducts(data: GetProduct[]): void {
    if(!!data) {
      this.dataSource.data = data;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    }
  }

}
