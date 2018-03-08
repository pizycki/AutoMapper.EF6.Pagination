Each of type represents different example of pagination

* _Customers_ - Fixed page
  
  The simplest form of pagination. Fixed page size, no sorting, page refreshed after clicking on another page.
  
* _Employers_ - Fixed page with sort

  Same as above, but with sorting by column.

* _Directors_ - Infinite page
  
  Page content is loaded automaticly once reaching certain point of page while scrolling down. This paging doesn't involve pager.

* _[ToBeDone] Managers_ - Only visible rows

  This particular example is common in mobile world where browser is limited by memory and every byte is crucial. It detects which rows should be loaded and dispose the rest.
