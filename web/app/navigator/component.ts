import { Injectable } from '@angular/core';

function _navigator(): any {
  // return the native navigator obj
  return navigator;
}

@Injectable()
export class NavigatorRef {
  
  get navigator(): any {
    return _navigator();
  }
  
}
