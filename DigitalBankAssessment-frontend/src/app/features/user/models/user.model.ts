export interface User {
  id: number;
  nombre: string;
  fechaNacimiento: Date;
  sexo: 'M' | 'F' | '';
  activo: boolean;
}