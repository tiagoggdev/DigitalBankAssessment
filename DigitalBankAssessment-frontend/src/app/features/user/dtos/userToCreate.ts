export interface UserToCreate {
  nombre: string;
  fechaNacimiento: Date;
  sexo: 'M' | 'F' | '';
}