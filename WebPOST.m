//  This is a class to do a post to a webserver

//  WebPOST.m
//  Objective-C
//
//  Created by Harper Lovelace on 10/18/11.


@interface NSURLRequest (DummyInterface)
+ (BOOL)allowsAnyHTTPSCertificateForHost:(NSString*)host;
+ (void)setAllowsAnyHTTPSCertificate:(BOOL)allow forHost:(NSString*)host;
@end

#import "WebPOST.h"

@implementation WebPOST

- (id)init
{
    self = [super init];
    if (self) {
        
        //Set the Initial Values
        FullURL = @"";
        QueryString = @"";
    }
    
    return self;
}


-(void) setURL:(NSString *) u {
    FullURL = u;
}

- (void) ElementName: (NSString*) sName ElementValue: (NSString*) sValue  {
    
    QueryString = [NSString stringWithFormat: @"%@%@=%@&", QueryString, sName, sValue]; 
    
}

-(void) Execute {
    
    //Remove the last "&"
    NSString *post = QueryString ;
    NSURL *url=[NSURL URLWithString:FullURL];
    
    
    NSData *postData = [post dataUsingEncoding:NSASCIIStringEncoding allowLossyConversion:YES];
    
    NSString *postLength = [NSString stringWithFormat:@"%d", [postData length]];
    
    NSMutableURLRequest *request = [[[NSMutableURLRequest alloc] init] autorelease];
    [request setURL:url];
    [request setHTTPMethod:@"POST"];
    [request setValue:postLength forHTTPHeaderField:@"Content-Length"];
    [request setValue:@"application/x-www-form-urlencoded" forHTTPHeaderField:@"Content-Type"];
    [request setHTTPBody:postData];
    
    
    [NSURLRequest setAllowsAnyHTTPSCertificate:YES forHost:[url host]];
    
    NSError *error;
    NSURLResponse *response;
    NSData *urlData=[NSURLConnection sendSynchronousRequest:request returningResponse:&response error:&error];
    
    NSString *data=[[NSString alloc]initWithData:urlData encoding:NSUTF8StringEncoding];
    NSLog(@"%@",data);  
    
    
}

@end
