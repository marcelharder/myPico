import { Photo } from './Photo';

export interface User {
  id: number;
  username: string;
  knownAs: string;
  age: number;
  databaseRole: string;
  email?: string;
  gender: string;
  created: Date;
  lastActive: Date;
  photoUrl: string;
  city: string;
  country: string;
  interests?: string;
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
  iban?: string;
}

