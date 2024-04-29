import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, shareReplay } from 'rxjs';
import { GetProductCategory, ProductCategory } from '../models/ProductCategory';
import { GetProduct, Product } from '../models/Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  errorMessages: string[] | undefined;
  productUrl = environment.apiUrl + 'products/';
  categoriesUrl = this.productUrl + 'categories/'

  constructor(private http: HttpClient) { }

  public getProductCategories(): Observable<GetProductCategory[]> {
    return this.http.get<GetProductCategory[]>(this.categoriesUrl).pipe(
      shareReplay(),
      catchError((err) => {
        this.onError(err);
        throw err;
      })
    )
  }

  public createProductCategories(category: ProductCategory): Observable<GetProductCategory> {
    return this.http.post<GetProductCategory>(this.categoriesUrl, category).pipe(
      shareReplay(),
      catchError((err) => {
        this.onError(err);
        throw err;
      })
    )
  }

  public updateProductCategory(categoryId: number, category: ProductCategory): Observable<GetProductCategory> {
    return this.http.put<GetProductCategory>(`${this.categoriesUrl}${categoryId}`, category).pipe(
      shareReplay(),
      catchError((err) => {
        this.onError(err);
        throw err;
      })
    );
  }

  public deleteProductCategory(categoryId: number): Observable<void> {
    return this.http.delete<void>(`${this.categoriesUrl}${categoryId}`).pipe(
      catchError((err) => {
        this.onError(err);
        throw err;
      })
    );
  }

  public getProducts(): Observable<GetProduct[]> {
    return this.http.get<GetProduct[]>(this.productUrl).pipe(
      shareReplay(),
      catchError((err) => {
        this.onError(err);
        throw err;
      })
    )
  }

  public createProduct(product: Product): Observable<GetProduct> {
    return this.http.post<GetProduct>(this.productUrl, product).pipe(
      shareReplay(),
      catchError((err) => {
        this.onError(err);
        throw err;
      })
    )
  }

  public updateProduct(productId: number, product: Product): Observable<GetProduct> {
    return this.http.put<GetProduct>(`${this.productUrl}${productId}`, product).pipe(
      shareReplay(),
      catchError((err) => {
        this.onError(err);
        throw err;
      })
    );
  }

  public deleteProduct(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.productUrl}${productId}`).pipe(
      catchError((err) => {
        this.onError(err);
        throw err;
      })
    );
  }

  private onError(err: any): void {
    if(err.error.errors) {
      this.errorMessages = err.error.errors;
    } else {
      this.errorMessages = [];
      this.errorMessages.push(err.error)
    }
  }
}
