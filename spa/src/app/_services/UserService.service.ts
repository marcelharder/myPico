import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/Message';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class UserService {

baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getMessages(id: number, page: string, itemsPerPage: string, messageContainer: string) {
  const paginatedResult: PaginatedResult = new PaginatedResult();
  let params = new HttpParams();
  params = params.append('MessageContainer', messageContainer);
  if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
  }
  return this.http.get<Message[]>(this.baseUrl + 'users/' + id + '/messages', { observe: 'response', params })
      .pipe(
          map(response => {
              if(response.body !== null){paginatedResult.result = response.body;}
              if (response.headers.get('Pagination') !== null) { 
                paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')); }
              return paginatedResult;
          }));
}
// tslint:disable-next-line: max-line-length
getMessageThread(id: number, recipientId: number) { return this.http.get<Message[]>(this.baseUrl + 'users/' + id + '/messages/thread/' + recipientId); }
sendMessage(id: number, message: Message) {
  return this.http.post(this.baseUrl + 'users/' + id + '/messages', message);
}
deleteMessage(id: number, userId: number) {
  return this.http.post(this.baseUrl + 'users/' + userId + '/messages/' + id, {});
}
markAsRead(userId: number, messageId: number) {
  return this.http.post(this.baseUrl + 'users/' + userId + '/messages/' + messageId + '/read', {}).subscribe();
}
getMessage(userId: number, messageId: number) {
  return this.http.get<Message>(this.baseUrl + 'users/' + userId + '/messages/' + messageId);
}
createMessage(userId: number) {
  return this.http.get<Message>(this.baseUrl + 'users/' + userId + '/createMessage');
}

}
