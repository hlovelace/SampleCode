//  This is a class to do a post to a webserver

//  WebPOST.h
//  Objective-C
//
//  Created by Harper Lovelace on 10/18/11.


#import <Foundation/Foundation.h>

@interface WebPOST : NSObject
{
    NSString *FullURL;
    NSString *QueryString;

}

//Set the URL to post to
- (void) setURL: (NSString*) s;

//The Element Name and Value to insert into the QueryString
- (void) ElementName: (NSString*) sName ElementValue: (NSString*) sValue;

//Execute the URL Post
- (void) Execute;


@end
