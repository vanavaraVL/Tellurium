import {IMenuItem} from './MenuItem';
import {RoutesEnums} from '../../types/enums/RoutesEnum';

export const MENU_ITEMS: IMenuItem[] = [
  {
    title: 'Home',
    link: `/${RoutesEnums.PAGES}/${RoutesEnums.ABOUT}`,
  },
  {
    title: 'Farm Management',
    link: `/${RoutesEnums.PAGES}/${RoutesEnums.ANIMALS}/view`,
  },
];
