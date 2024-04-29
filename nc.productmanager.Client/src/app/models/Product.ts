import { GetProductCategory } from "./ProductCategory";

export interface GetProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  category: GetProductCategory;
}

export interface Product {
  name: string;
  description: string;
  price: number;
  productCategoryId: number;
}
