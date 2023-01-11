import { Injectable } from '@angular/core';
import { Api } from '@core/enums/api.enum';
import { environment } from '@env/environment';
import { catchError, Observable, of } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MyComment, MainOffer } from '@modules/offers/interfaces/offers.interface';
import { ToastMessageService } from '@shared/modules/toast-message/services/toast-message.service';

@Injectable()
export class OffersService {

  constructor(
    private http: HttpClient,
    private toastMessageService: ToastMessageService,
  ) { }

  getAllOffers(countyId: number, search: string, categoryId: number, page: number, pageSize: number): Observable<MainOffer> {
    const params = new HttpParams()
      .set('countyId', countyId)
      .set('productName', search ? search : '')
      .set('categoryId', categoryId ? categoryId : '')
      .set('pageSize', pageSize)
      .set('pageNumber', page)

    return this.http.get<MainOffer>(`${environment.httpBackend}${Api.OFFERS}`, { params }).pipe(
      catchError(() => {
        this.toastMessageService.notifyOfError("Błąd pobierania ofert");
        return of();
      }),
    );
  }

  getFavourites(favouritesId: number, page: number, pageSize: number): Observable<MainOffer> {
    const params = new HttpParams()
      .set('pageSize', pageSize)
      .set('pageNumber', page)

    return this.http.get<MainOffer>(`${environment.httpBackend}${Api.FAVOURITES}`
      .replace(':favouritesId', favouritesId.toString()), { params })
      .pipe(
        catchError(() => {
          this.toastMessageService.notifyOfError("Błąd aktualizacji ulubionych");
          return of();
        }),
      );
  }

  updateFavourites(offerId: number, favouritesId: number): Observable<any> {
    const params = new HttpParams()
      .set('offerId', offerId)
      .set('favouritesId', favouritesId)

    return this.http.post<any>(`${environment.httpBackend}${Api.FAVOURITES_UPDATE}`, { params }).pipe(
      catchError(() => {
        this.toastMessageService.notifyOfError("Błąd pobierania ulubionych");
        return of();
      }),
    );
  }

  getComments(offerId: number, userId: number): Observable<MyComment[]> {
    const params = new HttpParams()
      .set('offerId', offerId)
      .set('userId', userId)

    return this.http.get<MyComment[]>(`${environment.httpBackend}${Api.OFFER_COMMENTS}`, { params }).pipe(
      catchError(() => {
        this.toastMessageService.notifyOfError("Błąd pobierania komentarzy");
        return of([]);
      }),
    );
  }

  addComment(content: string, userId: number, offerId: number): Observable<any> {
    const creationDate = new Date().toISOString();
    return this.http.post<any>(`${environment.httpBackend}${Api.COMMENT}`, { content, userId, offerId, creationDate }).pipe(
      catchError(() => {
        this.toastMessageService.notifyOfError("Błąd dodawania komentarza");
        return of();
      }),
    );
  }

  like(commentId: number): Observable<any> {
    return this.http.put<any>(`${environment.httpBackend}${Api.COMMENT_LIKE}`.replace(':id', commentId.toString()), {}).pipe(
      catchError(() => {
        this.toastMessageService.notifyOfError("Błąd polubienia");
        return of();
      }),
    );
  }

  dislike(commentId: number): Observable<any> {
    return this.http.put<any>(`${environment.httpBackend}${Api.COMMENT_DISLIKE}`.replace(':id', commentId.toString()), {}).pipe(
      catchError(() => {
        this.toastMessageService.notifyOfError("Błąd polubienia");
        return of();
      }),
    );
  }
}