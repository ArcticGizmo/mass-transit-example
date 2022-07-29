# What does?
This repo is broken into three parts
- MTCore - Contracts and other common details
- MTConsumer - A "Message" Consumer
- MTProducer - A "Message" Producer

For this example we will be using a docker hosted RabbitMQ instance (because it actually works), though other solutions should be relatively interchangeable.

## Exchanges
Exchanges are named based on your contracts. In this project there is a contract for `MTCore.Contracts.Message` so an RMQ exchange will be created with the same data

## Queues
Queues are created based on the consumer name. Since there is `MTConsumer.Consumers.MyConsumer`, it will be listening to the `my` queue, since it is the prefix of `Consumer`

## Errors
If your consumer encounters an error, messages are moved into the `{queue_name}_error` queue. Why? If your application faces a message that it cannot handle, it is moved into the separate queue so that it can try and process subsequent onces. This helps prevent bottlenecking, but you should be aware of this as an invalid error and a "I cannot push to my database right now" error will both be moved to the error queue.

# Start RMQ
```
docker run --rm -d --name rmq -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```
And trail the logs with
```
docker logs rmq --follow
```

# Stop RMQ
```
docker stop rmq
```

# Log into RMQ
You can access the dashboard [here](http://localhost:15672/) with `guest`/`guest`