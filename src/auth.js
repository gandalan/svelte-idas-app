import { IDASFactory } from '@gandalan/weblibs';

if (window.location.search) {
    IDASFactory.authorize();
}