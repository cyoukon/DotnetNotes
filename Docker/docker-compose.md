# docker-compose中的重试策略

| <font color='red'>restart policies</font> | <font color='red'>comment</font>                             |
| ----------------------------------------- | ------------------------------------------------------------ |
| **“no”**                                  | Never attempt to restart a container even if it crashes or stops altogether |
| **always**                                | If the container stops for any reasons whatsoever, always attempt to restart it |
| **on-failure**                            | Only attempt to restart the container if it failed because of an error code |
| **unless-stopped**                        | Always restart the container unless we (the developers) stop it explicitly. |

> Note here that the “no” restart policy explicitly has opening and closing quotes. This is because in a YAML file, a plain *no* is interpreted as *false*. Hence, to avoid the confusion, if we use the **no restart policy**, we have to always specify it as “no”.