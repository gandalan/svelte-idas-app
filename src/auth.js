import { IDASFactory } from './lib/IDAS';

if (window.location.search) {
    IDASFactory.authorize();
}